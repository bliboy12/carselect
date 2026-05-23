public class FavoriteSqlMapper
{
    public static FavoriteModel MapToDomein(FavoriteDataModelSQL favoriteDataModelSQL)
    {
        return new FavoriteModel
        {
            UserId = favoriteDataModelSQL.UserId,
            ListingId = favoriteDataModelSQL.ListingId
        };
    }
    public static FavoriteDataModelSQL MapFromDomein(FavoriteModel favoriteModel)
    {
        return new FavoriteDataModelSQL
        {
            UserId = favoriteModel.UserId,
            ListingId = favoriteModel.ListingId,
            User = UserSqlMapper.MapFromDomein(favoriteModel.User!)
        };
    }
}