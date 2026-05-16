public class ListingService : IListingService
{
    private readonly ICarService _carService;
    private readonly IListingRepository _listingRepo;
    private readonly ICarImageService _carImageSerivce;
    private readonly IUserService _userService;
    public ListingService(IListingRepository listingRepository, ICarImageService carImageService, ICarService carService, IUserService userService)
    {
        _listingRepo = listingRepository;
        _carImageSerivce = carImageService;
        _carService = carService;
        _userService = userService;
    }

    public async Task<ListingModel> CreateListingAsync(ListingModel listingModel)
    {
        var carResult = await _carService.CreateCarAsync(listingModel.Car);
        // we try to create listing after having made the car and if it fails
        // we make sure to delete the car associated to it insuring that we don't have any cars floating around without any reference
        try
        {
            // assign the newly created cars id to the listing carId reference
            listingModel.CarId = carResult.Id;
            // creating a new Id at assigning it to the new listingId that will be send out to the repo to create
            listingModel.Id = Guid.NewGuid();
            // Keeping it UTC insure different dateTime that don't align (if the users are in different TimeZones), we keep it universal
            listingModel.CreatedAt = DateTime.UtcNow;
            listingModel.UpdatedAt = listingModel.CreatedAt;

            var listingResult = await _listingRepo.CreateListingAsync(ListingMapper.MapFromDomein(listingModel));
            var userResult = await _userService.GetUserByIdAsync(listingModel.SellerId);

            ListingModel listingData = ListingMapper.MapToDomein(listingResult);
            listingData.Car = carResult;
            listingData.Seller = userResult;

            return listingData;
        }
        catch (Exception ex)
        {
            await _carService.DeleteCarByIdAsync(carResult.Id);
            throw new Exception(ex.Message);
        }

    }

    public async Task DeletelistingById(Guid listingId)
    {
        // We get the car ID from this listing AND delete it
        ListingModel listingModel = await GetListingByIdAsync(listingId);
        await _carService.DeleteCarByIdAsync(listingModel.CarId);

        // Remove all the images associated with the listingId
        await _carImageSerivce.DeleteAllImagesByListingIdAsync(listingId);

        // After deleting the car images, the listing needs to be deleted
        await _listingRepo.DeleteListingByIdAsync(listingId.ToString());
    }

    public async Task<IEnumerable<FavoriteModel>> GetAllFavoritesByListingIdAsync(Guid userId, Guid listingId)
    {
        var results = await _listingRepo.GetAllFavoritesByListingIdAsync(userId.ToString(), listingId.ToString());
        List<FavoriteModel> favorites = new();

        foreach (FavoriteDataModel favorite in results)
            favorites.Add(FavoriteMapper.MapToDomein(favorite));

        return favorites;
    }

    public async Task<IEnumerable<ListingModel>> GetAllListingsAsync()
    {
        var results = await _listingRepo.GetAllListingsAsync();

        // Going through each listing in results in async way
        IEnumerable<Task<ListingModel>> listingTasks = results.Select(async listing =>
        {
            ListingModel listingModel = ListingMapper.MapToDomein(listing);

            var carTask = _carService.GetCarByIdAsync(listingModel.CarId);
            var sellerTask = _userService.GetUserByIdAsync(listingModel.SellerId);
            var carImageTask = _carImageSerivce.GetAllCarImagesByListingIdAsync(listingModel.Id);

            // Creates a task that will complete when all of the supplied tasks have completed
            await Task.WhenAll(sellerTask, carTask, carImageTask);

            // We await to ensure that we don't try to assign before we have received a response
            listingModel.Car = await carTask;
            listingModel.Seller = await sellerTask;
            listingModel.CarImages = await carImageTask;

            return listingModel;
        });
        // then we tell it to complete the task of listingTasks which holds all the looped over tasks, in one go.
        // making a DB call in one go, so if there is 60 listings that would mean 60 DB calls
        // Which isn't good, need to be changed!!
        return await Task.WhenAll(listingTasks);
    }

    public async Task<IEnumerable<ListingModel>> GetAllListingsBySellerIdAsync(Guid sellerId)
    {
        var results = await _listingRepo.GetAllListingsBySellerIdAsync(sellerId.ToString());
        List<ListingModel> listings = new();

        foreach (ListingDataModel listing in results)
        {
            ListingModel listingModel = ListingMapper.MapToDomein(listing);

            var sellerTask = _userService.GetUserByIdAsync(listingModel.SellerId);
            var carTask = _carService.GetCarByIdAsync(listingModel.CarId);
            var carImageTask = _carImageSerivce.GetAllCarImagesByListingIdAsync(listingModel.Id);

            // Creates a task that will complete when all of the supplied tasks have completed
            await Task.WhenAll(sellerTask, carTask, carImageTask);

            // We await to ensure that we don't try to assign before we have received a response
            listingModel.Car = await carTask;
            listingModel.Seller = await sellerTask;
            listingModel.CarImages = await carImageTask;

            listings.Add(listingModel);

        }
        return listings;
    }

    public async Task<ListingModel> GetListingByIdAsync(Guid listingId)
    {
        var result = await _listingRepo.GetListingByIdAsync(listingId.ToString());
        ListingModel listingModel = ListingMapper.MapToDomein(result);

        // The "n+1" problem ==> this is a problem when its a big catalog where you fetch for example:
        // 20 listings -- which means --> +20 users, + +20 sellers, (+20 carImages * n, because could be multiple carimages)
        // which results in a lot of calls. 

        var carTask = _carService.GetCarByIdAsync(listingModel.CarId);
        var sellerTask = _userService.GetUserByIdAsync(listingModel.SellerId);
        var carImageTask = _carImageSerivce.GetAllCarImagesByListingIdAsync(listingId);

        // Creates a task that will complete when all of the supplied tasks have completed
        await Task.WhenAll(sellerTask, carTask, carImageTask);

        // We await to ensure that we don't try to get until they have finished
        listingModel.Car = await carTask;
        listingModel.Seller = await sellerTask;
        listingModel.CarImages = await carImageTask;

        return listingModel;
    }

    public async Task<ListingModel> UpdateListingAsync(ListingModel listingModel)
    {
        var getOldListing = await _listingRepo.GetListingByIdAsync(listingModel.Id.ToString());
        listingModel.CreatedAt = getOldListing.CreatedAt;
        var result = await _listingRepo.UpdateListingAsync(ListingMapper.MapFromDomein(listingModel));

        return ListingMapper.MapToDomein(result);
    }
    // This will be changed after refactoring the Database
    public async Task<IEnumerable<ListingModel>> FilterListingsByCarAsync(CarFilterModel filter)
    {
        var listings = await GetAllListingsAsync();

        return listings.Where(l =>
            (string.IsNullOrEmpty(filter.Brand) || l.Car.Brand.ToLower() == filter.Brand) &&
            (string.IsNullOrEmpty(filter.Model) || l.Car.Model.ToLower() == filter.Model) &&
            (string.IsNullOrEmpty(filter.Color) || l.Car.Color.ToLower() == filter.Color) &&
            (string.IsNullOrEmpty(filter.Trim) || l.Car.Trim.ToLower() == filter.Trim) &&
            (filter.YearFrom == null || l.Car.BuildYear >= filter.YearFrom) &&
            (filter.YearTo == null || l.Car.BuildYear <= filter.YearTo) &&
            (filter.Fuel == null || l.Car.Fuel == filter.Fuel) &&
            (filter.Transmission == null || l.Car.Transmission == filter.Transmission) &&
            (filter.MinKilometers == null || l.Car.Kilometers >= filter.MinKilometers) &&
            (filter.MaxKilometers == null || l.Car.Kilometers <= filter.MaxKilometers) &&
            (filter.Doors == null || l.Car.Doors == filter.Doors) &&
            (filter.Drive == null || l.Car.Drive == filter.Drive)
        );
    }

    public async Task DeleteAllListingsBySellerIdAsync(Guid sellerId)
    {
        var allListingsBySellerId = await _listingRepo.GetAllListingsBySellerIdAsync(sellerId.ToString());
        var deleteListing = allListingsBySellerId.Select(async (l) => await _listingRepo.DeleteListingByIdAsync(l.Id));

        await Task.WhenAll(deleteListing);
    }
}