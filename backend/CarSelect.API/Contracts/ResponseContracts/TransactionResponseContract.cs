public class PurchaseResponseContract
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public Guid ListingId { get; set; }
    public decimal AgreedPrice { get; set; }
    public DateTime TransactionDate { get; set; }
    public string StripeId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}