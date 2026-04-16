public class PurchaseModel
{
    public Guid Id { get; set; }
    public int BuyerId { get; set; }
    public int ListingId { get; set; }
    public decimal? AgreedPrice { get; set; }
    public DateTime PurchaseDate { get; set; }

    public UserModel Buyer { get; set; } = null!;
    public ListingModel Listing { get; set; } = null!;
}