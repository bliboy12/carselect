using System.Text.Json.Serialization;

public class ReviewDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    public string SellerId { get; set; } = string.Empty;
    public string ReviewerId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}