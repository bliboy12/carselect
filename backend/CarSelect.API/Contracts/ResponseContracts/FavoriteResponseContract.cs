public class FavoriteReponseContract
{
    public Guid UserId { get; set; }
    public ListingResponseContract Listing { get; set; } = new();
}