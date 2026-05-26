public class ReviewRequestContract
{
    public Guid SellerId { get; set; }
    public Guid ReviewerId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}