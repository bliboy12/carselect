public class FavoriteApiMapper
{
    public static FavoriteReponseContract MapToContract(FavoriteModel favoriteModel)
    {
        return new FavoriteReponseContract
        {
            UserId = favoriteModel.UserId,
            ListingId = favoriteModel.ListingId
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