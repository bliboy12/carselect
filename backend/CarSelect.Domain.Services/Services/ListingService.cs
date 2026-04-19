public class ListingService : IListingService
{
    private readonly IListingRepository _listingRepo;
    private readonly ICarImageService _carImageSerivce;
    public ListingService(IListingRepository listingRepository, CarImageService carImageService)
    {
        _listingRepo = listingRepository;
        _carImageSerivce = carImageService;
    }

    public async Task<ListingModel> CreateListingAsync(ListingModel listingModel)
    {
        ListingDataModel result;
        // This is to prevent a carImages to be created in Blob without a listing. If listing fails we can't create images to blob
        try
        {
            result = await _listingRepo.CreateListingAsync(ListingMapper.MapFromDomein(listingModel));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
            throw;
        }
        var createdImage = _carImageSerivce.CreateCarImage()
    }

    public Task DeletelistingById(Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FavoriteModel>> GetAllFavoritesWithListingIdAsync(Guid userId, Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListingModel>> GetAllListingsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListingModel>> GetAllListingsBySellerIdAsync(Guid sellerId)
    {
        throw new NotImplementedException();
    }

    public Task<ListingModel> GetListingByIdAsync(Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task<ListingModel> UpdateListingAsync(ListingModel listingModel)
    {
        throw new NotImplementedException();
    }
}