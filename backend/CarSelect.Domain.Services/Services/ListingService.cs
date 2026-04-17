public class ListingService : IListingService
{
    public Task<CarImage> AddCarImageAsync(CarImage carImageData)
    {
        throw new NotImplementedException();
    }

    public Task<ListingModel> AddListing(ListingModel listingData)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CarImage>> GetAllCarImagesByListingIdAsync(Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FavoritesModel>> GetAllFavoritesWithListingIdAsync(Guid userId, Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListingModel>> GetAllListings()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListingModel>> GetAllListingsBySellerId(Guid sellerId)
    {
        throw new NotImplementedException();
    }

    public Task<CarImage?> GetFirstCarImageByListingIdAsync(Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task<ListingModel> GetListingById(Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task<ListingModel> GetListingWithId(Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCarImageAsync(Guid carImageId)
    {
        throw new NotImplementedException();
    }

    public Task<CarImage> UpdateCarImageAsync(CarImage carImageData)
    {
        throw new NotImplementedException();
    }
}