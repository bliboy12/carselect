public class FavoriteDataModelSQL
{
    public Guid UserId { get; set; }
    public Guid ListingId { get; set; }
    public UserDataModelSQL User { get; set; } = null!;
}