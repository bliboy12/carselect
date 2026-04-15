public interface IFavoriteRepository
{
    FavoriteDataModel AddFavorite(FavoriteDataModel favoriteData);
    FavoriteDataModel GetFavoriteWithId(int favoriteId);
    ICollection<FavoriteDataModel> GetAllFavoritesWithUserId(int userId);
    FavoriteDataModel UpdateFavorite(FavoriteDataModel favoriteData);
    void RemoveFavorite(int favoriteId);
}