public interface IListingService
{
    Task<ListingModel> AddListing(ListingModel listingData);
    Task<ListingModel> GetListingWithId(Guid listingId);
    Task<IEnumerable<ListingModel>> GetAllListingsBySellerId(Guid sellerId);
    Task<ListingModel> GetListingById(Guid listingId);
    Task<IEnumerable<ListingModel>> GetAllListings();

    // Car Images for the associated listing
    Task<CarImage> AddCarImageAsync(CarImage carImageData);
    Task<CarImage> UpdateCarImageAsync(CarImage carImageData);
    Task<IEnumerable<CarImage>> GetAllCarImagesByListingIdAsync(Guid listingId);
    Task<CarImage?> GetFirstCarImageByListingIdAsync(Guid listingId); // TO BE SEEN
    Task RemoveCarImageAsync(Guid carImageId);

    // Amount of Favorited of this listing
    Task<IEnumerable<FavoritesModel>> GetAllFavoritesWithListingIdAsync(Guid userId, Guid listingId);
}