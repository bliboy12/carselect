public class FavoriteApiMapper
{
    public static FavoriteReponseContract MapToContract(FavoriteModel favoriteModel)
    {
        return new FavoriteReponseContract
        {
            UserId = favoriteModel.UserId,
            Listing = ListingApiMapper.MapToContract(favoriteModel.Listing)
        };
    }
    public static FavoritesReponseContract MapToContract(Guid userId, IEnumerable<FavoriteModel> favoriteModel)
    {
        return new FavoritesReponseContract
        {
            UserId = userId,
            Listings = favoriteModel.Select((l) => ListingApiMapper.MapToContract(l.Listing))
        };
    }
    public static FavoriteModel MapToDomain(FavoriteRequestContract favoriteRequest)
    {
        return new FavoriteModel
        {
            UserId = favoriteRequest.UserId,
            ListingId = favoriteRequest.ListingId
        };
    }

}