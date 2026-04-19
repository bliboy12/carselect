using System.Text.Json.Serialization;

public class CarImageDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    public string ListingId { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMainImage { get; set; }
}