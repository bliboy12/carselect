public class UserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime RegisterDate { get; set; }
    public bool IsAdmin { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<ReviewModel> ReceivedReviews { get; set; } = new List<ReviewModel>();
    public ICollection<ReviewModel> WrittenReviews { get; set; } = new List<ReviewModel>();
    public ICollection<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
    public ICollection<FavoriteModel> Favorites { get; set; } = new List<FavoriteModel>();
}