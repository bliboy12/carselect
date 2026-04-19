public interface IListingService
{
    Task<ListingModel> CreateListingAsync(ListingModel listingModel);
    Task<IEnumerable<ListingModel>> GetAllListingsBySellerIdAsync(Guid sellerId);
    Task<ListingModel> GetListingByIdAsync(Guid listingId);
    Task<IEnumerable<ListingModel>> GetAllListingsAsync();
    Task<ListingModel> UpdateListingAsync(ListingModel listingModel);
    Task DeletelistingById(Guid listingId);

    // Car Images for the associated listing will be in a dedicated Service Class
    // Task<CarImageModel> CreateCarImageAsync(CarImageModel carImageData);
    // Task<CarImageModel> UpdateCarImageAsync(CarImageModel carImageData);
    // Task<IEnumerable<CarImageModel>> GetAllCarImagesByListingIdAsync(Guid listingId);
    // Task<CarImageModel?> GetFirstCarImageByListingIdAsync(Guid listingId); // TO BE SEEN
    // Task RemoveCarImageAsync(Guid carImageId);

    // Amount of Favorited of this listing
    Task<IEnumerable<FavoriteModel>> GetAllFavoritesWithListingIdAsync(Guid userId, Guid listingId);
}