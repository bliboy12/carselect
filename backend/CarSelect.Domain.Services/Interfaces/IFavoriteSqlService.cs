public interface IFavoriteSqlService
{

    Task<FavoriteModel> CreateFavoriteAsync(Guid userId, Guid listingId);

    Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserIdAsync(Guid userId);

    Task<bool> IsFavoritedAsync(Guid userId, Guid listingId);
    Task DeleteFavoriteByListingIdAsync(Guid userId, Guid listingId);
}