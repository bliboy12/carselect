public class PurchaseDataModel
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public int ListingId { get; set; }
    public decimal? AgreedPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}