public class FavoriteModel
{
    public Guid UserId { get; set; }
    public Guid ListingId { get; set; }
    public UserModel? User { get; set; } = null!;
    public ListingModel? Listing { get; set; } = null!;
}