public class FavoriteMapper
{
    public static FavoriteModel MapToDomein(FavoriteDataModel favoriteDataModel)
    {
        return new FavoriteModel
        {
            UserId = Guid.Parse(favoriteDataModel.UserId),
            ListingId = Guid.Parse(favoriteDataModel.ListingId)
        };
    }
    public static FavoriteDataModel MapFromDomein(FavoriteModel favoriteModel)
    {
        return new FavoriteDataModel
        {
            Id = $"{favoriteModel.UserId}_{favoriteModel.ListingId}",
            UserId = favoriteModel.UserId.ToString(),
            ListingId = favoriteModel.ListingId.ToString(),
        };
    }
}