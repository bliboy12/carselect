public class ReviewRequestContract
{
    public Guid SellerId { get; set; }
    public Guid ReviewerId { get; set; }
    public Guid Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}