public class ListingRequestContract
{
    public Guid SellerId { get; set; }
    public Guid CarId { get; set; }
    public decimal Price { get; set; }
    public List<CarImageRequestContract> CarImages { get; set; } = new();
}