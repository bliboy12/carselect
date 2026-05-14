public class ListingRequestContract
{
    public required Guid SellerId { get; set; }
    public required Guid CarId { get; set; }
    public required decimal Price { get; set; }
    public required ListingStatusTypeContract Status { get; set; }
    //public List<CarImageRequestContract> CarImages { get; set; } = new();
}