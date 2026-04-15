public class FavoritesModel
{
    public int Id { get; set; }
    public UserModel User { get; set; } = null!;
    public ListingModel Listing { get; set; } = null!;
}