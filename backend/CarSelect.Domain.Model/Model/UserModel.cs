public class UserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime RegisterDate { get; set; }
    public bool IsAdmin { get; set; } = false;

    public ICollection<ReviewModel> ReceivedReviews { get; set; } = new List<ReviewModel>();
    public ICollection<ReviewModel> WrittenReviews { get; set; } = new List<ReviewModel>();
    public ICollection<PurchaseModel> Purchases { get; set; } = new List<PurchaseModel>();
    public ICollection<FavoritesModel> Favorites { get; set; } = new List<FavoritesModel>();
}