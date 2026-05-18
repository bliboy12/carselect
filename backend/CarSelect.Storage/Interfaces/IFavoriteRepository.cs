public interface IFavoriteRepository
{
    // Favorites
    Task<FavoriteDataModel> CreateFavoriteAsync(FavoriteDataModel favoriteData);
    Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesByUserIdAsync(string userId);
    Task<bool> IsFavoritedAsync(string userId, string listingId); // Does a certain favorite exist
    Task DeleteFavoriteAsyncByListingIdAsync(string userId, string listingId);
}