using System.Text.Json.Serialization;

public class FavoriteDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int UserId { get; set; }
    public int ListingId { get; set; }
}