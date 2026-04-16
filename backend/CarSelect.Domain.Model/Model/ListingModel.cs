public class ListingModel
{
    public Guid Id { get; set; }
    public int SellerId { get; set; }
    public int CarId { get; set; }
    public double Price { get; set; }
    public DateTime ListedDate { get; set; }
    public ListingStatus Status { get; set; }

    public UserModel Seller { get; set; } = null!;
}