using System.Text.Json.Serialization;

public class CarImageDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int ListingId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}