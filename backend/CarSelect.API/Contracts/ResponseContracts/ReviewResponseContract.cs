public class ReviewResponseContract
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid ReviewerId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}