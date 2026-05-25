public class EnrichedReviewResponseContract
{
    public Guid SellerId { get; set; }
    public Guid ReviewerId { get; set; }
    public string ReviewerFirstName { get; set; } = string.Empty;
    public string ReviewerLastName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}