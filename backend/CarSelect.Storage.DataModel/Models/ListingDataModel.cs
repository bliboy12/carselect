using System.Text.Json.Serialization;

public class ListingDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int SellerId { get; set; }
    public int CarId { get; set; }
    public double Price { get; set; }
    public DateTime ListedDate { get; set; }
    public string Status { get; set; } = string.Empty;

}