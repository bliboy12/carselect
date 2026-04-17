public class ListingModel
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid CarId { get; set; }
    public decimal Price { get; set; }
    public DateTime ListedDate { get; set; }
    public ListingStatus Status { get; set; }
    public UserModel? Seller { get; set; } = null!;

    public ICollection<CarImageModel> CarImages { get; set; } = [];
}