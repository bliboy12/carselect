public class ListingModel
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid CarId { get; set; }
    public CarModel Car { get; set; } = new();
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ListingStatus Status { get; set; }
    public UserModel? Seller { get; set; } = null!;

    public List<CarImageModel> CarImages { get; set; } = new();
}