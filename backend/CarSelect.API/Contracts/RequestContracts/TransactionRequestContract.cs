public class TransactionRequestConctract
{
    public Guid BuyerId { get; set; }
    public Guid ListingId { get; set; }
    public decimal AgreedPrice { get; set; }
    public DateTime TransactionDate { get; set; }
    public string StripeId { get; set; } = string.Empty;
    public TransactionStatusTypeContract Status { get; set; }
}