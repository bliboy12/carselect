public class ListingResponseContract
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid CarId { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Status { get; set; } = "Active";
}