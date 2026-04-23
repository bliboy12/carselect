public class ListingService : IListingService
{
    private readonly ICarService _carService;
    private readonly IListingRepository _listingRepo;
    private readonly ICarImageService _carImageSerivce;
    public ListingService(IListingRepository listingRepository, ICarImageService carImageService, ICarService carService)
    {
        _listingRepo = listingRepository;
        _carImageSerivce = carImageService;
        _carService = carService;
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
            return ListingMapper.MapToDomein(listingResult);
        }
        catch (Exception ex)
        {
            await _carService.DeleteCarByIdAsync(carResult.Id);
            throw new Exception(ex.Message);
        }

    }

    public async Task DeletelistingById(Guid listingId)
    {
        await _carImageSerivce.DeleteAllImagesByListingIdAsync(listingId);
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
        List<ListingModel> listings = new();

        foreach (ListingDataModel listing in results)
            listings.Add(ListingMapper.MapToDomein(listing));

        return listings;
    }

    public async Task<IEnumerable<ListingModel>> GetAllListingsBySellerIdAsync(Guid sellerId)
    {
        var results = await _listingRepo.GetAllListingsBySellerIdAsync(sellerId.ToString());
        List<ListingModel> listings = new();

        foreach (ListingDataModel listing in results)
            listings.Add(ListingMapper.MapToDomein(listing));

        return listings;
    }

    public async Task<ListingModel> GetListingByIdAsync(Guid listingId)
    {
        var result = await _listingRepo.GetListingByIdAsync(listingId.ToString());

        return ListingMapper.MapToDomein(result);
    }

    public async Task<ListingModel> UpdateListingAsync(ListingModel listingModel)
    {
        var getOldListing = await _listingRepo.GetListingByIdAsync(listingModel.Id.ToString());
        listingModel.CreatedAt = getOldListing.CreatedAt;
        var result = await _listingRepo.UpdateListingAsync(ListingMapper.MapFromDomein(listingModel));

        return ListingMapper.MapToDomein(result);
    }
}