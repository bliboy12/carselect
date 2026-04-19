using System.Text.Json.Serialization;

public class FavoriteDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ListingId { get; set; } = string.Empty;
}