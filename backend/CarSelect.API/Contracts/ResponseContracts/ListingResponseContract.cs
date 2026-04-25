public class ListingResponseContract
{
    public Guid Id { get; set; }
    public SellerResponseContract Seller { get; set; } = new();
    public CarResponseContract Car { get; set; } = new();
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Status { get; set; } = "Active";
    public IEnumerable<CarImageResponseContract> CarImages { get; set; } = [];
}