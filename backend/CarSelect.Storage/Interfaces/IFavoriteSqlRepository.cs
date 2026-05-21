public interface IFavoriteSqlRepository
{
    Task<FavoriteDataModelSQL> CreateFavoriteAsync(FavoriteDataModelSQL favorite);

    Task<IEnumerable<FavoriteDataModelSQL>> GetAllFavoritesByUserIdAsync(Guid userId);

    Task<bool> IsFavoritedAsync(Guid userId, Guid listingId);

    Task DeleteFavoriteByListingIdAsync(Guid userId, Guid listingId);
}