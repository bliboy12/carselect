public class ListingDataModel
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public int CarId { get; set; }
    public double Price { get; set; }
    public DateTime ListedDate { get; set; }
    public string Status { get; set; } = string.Empty;

}