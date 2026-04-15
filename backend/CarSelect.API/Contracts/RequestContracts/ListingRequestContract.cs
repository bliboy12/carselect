public class ListingRequestContract
{
    public int SellerId { get; set; }
    public int CarId { get; set; }
    public double Price { get; set; }
    public string Status { get; set; } = "Active";
}