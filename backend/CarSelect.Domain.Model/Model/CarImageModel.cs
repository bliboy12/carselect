public class CarImageModel
{
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMainImage { get; set; }
}