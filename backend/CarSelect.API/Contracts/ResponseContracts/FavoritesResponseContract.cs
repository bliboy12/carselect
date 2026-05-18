public class FavoritesReponseContract
{
    public Guid UserId { get; set; }
    public IEnumerable<ListingResponseContract> Listings { get; set; } = [];
}