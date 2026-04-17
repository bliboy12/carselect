using System.Text.Json.Serialization;

public class ListingDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SellerId { get; set; } = string.Empty;
    public string CarId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime ListedDate { get; set; }
    public string Status { get; set; } = string.Empty;

}