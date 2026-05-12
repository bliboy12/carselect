public class DetailedListingRequestContract
{
    public required Guid SellerId { get; set; }
    public required CarRequestContract Car { get; set; }
    public required decimal Price { get; set; }
    public required ListingStatusTypeContract Status { get; set; }
    public List<CarImageRequestContract> CarImages { get; set; } = new();
}