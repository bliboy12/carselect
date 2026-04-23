public interface IListingRepository
{
    Task<ListingDataModel> CreateListingAsync(ListingDataModel listingData);
    Task<ListingDataModel> GetListingByIdAsync(string listingId);
    Task<IEnumerable<ListingDataModel>> GetAllListingsBySellerIdAsync(string sellerId);
    Task<IEnumerable<ListingDataModel>> GetAllListingsAsync();
    Task<ListingDataModel> UpdateListingAsync(ListingDataModel listing);
    Task DeleteListingByIdAsync(string listingId);

    // Car Images for the associated listing will be handle else where.
    // Task<CarImageDataModel> AddCarImageAsync(CarImageDataModel carImageData);
    // Task<CarImageDataModel> UpdateCarImageAsync(CarImageDataModel carImageData);
    // Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(string listingId);
    // Task<CarImageDataModel?> GetFirstCarImageByListingIdAsync(string listingId); // TO BE SEEN
    // Task RemoveCarImageAsync(string carImageId);

    // Amount of Favorited of this listing
    Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesByListingIdAsync(string userId, string listingId);

}