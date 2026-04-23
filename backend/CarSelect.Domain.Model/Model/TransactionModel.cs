public class TransactionModel
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public Guid ListingId { get; set; }
    public decimal AgreedPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
    //public string StripeId { get; set; } = string.Empty;
    public TransactionStatus Status { get; set; }

    public UserModel? Buyer { get; set; } = null!;
    public ListingModel? Listing { get; set; } = null!;
}