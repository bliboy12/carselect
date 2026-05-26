public class TransactionModel
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public Guid ListingId { get; set; }
    public decimal AgreedPrice { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Status { get; set; } = string.Empty;
}