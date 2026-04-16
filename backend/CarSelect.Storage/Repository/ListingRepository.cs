public class ListingRepository : IListingRepository
{
    public Task<CarImageDataModel> AddCarImageAsync(CarImageDataModel carImageData)
    {
        throw new NotImplementedException();
    }

    public Task<ListingDataModel> AddListing(ListingDataModel listingData)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesWithListingIdAsync(int userId, int listingId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListingDataModel>> GetAllListings()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListingDataModel>> GetAllListingsBySellerId(int sellerId)
    {
        throw new NotImplementedException();
    }

    public Task<CarImageDataModel?> GetFirstCarImageByListingIdAsync(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task<ListingDataModel> GetListingById(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task<ListingDataModel> GetListingWithId(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCarImageAsync(int carImageId)
    {
        throw new NotImplementedException();
    }

    public Task<CarImageDataModel> UpdateCarImageAsync(CarImageDataModel carImageData)
    {
        throw new NotImplementedException();
    }
}