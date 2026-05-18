public interface IFavoriteService
{
    // Favorites
    Task<FavoriteModel> CreateFavoriteAsync(Guid userId, Guid listingId);
    Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserIdAsync(Guid userId);
    Task<bool> IsFavoritedAsync(Guid userId, Guid listingId); // Does a certain favorite exist
    Task DeleteFavoriteByListingIdAsync(Guid userId, Guid listingId);
}