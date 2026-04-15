public class ReviewModel
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public int ReviewerId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public UserModel Seller { get; set; } = null!;
    public UserModel Reviewer { get; set; } = null!;
}