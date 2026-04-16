public interface IListingRepository
{
    Task<ListingDataModel> AddListing(ListingDataModel listingData);
    Task<ListingDataModel> GetListingWithId(int listingId);
    Task<IEnumerable<ListingDataModel>> GetAllListingsBySellerId(int sellerId);
    Task<ListingDataModel> GetListingById(int listingId);
    Task<IEnumerable<ListingDataModel>> GetAllListings();

    // Car Images for the associated listing
    Task<CarImageDataModel> AddCarImageAsync(CarImageDataModel carImageData);
    Task<CarImageDataModel> UpdateCarImageAsync(CarImageDataModel carImageData);
    Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(int listingId);
    Task<CarImageDataModel?> GetFirstCarImageByListingIdAsync(int listingId); // TO BE SEEN
    Task RemoveCarImageAsync(int carImageId);

    // Amount of Favorited of this listing
    Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesWithListingIdAsync(int userId, int listingId);

}