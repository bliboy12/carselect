
using Newtonsoft.Json;

public class ReviewDataModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("sellerId")]
    public string SellerId { get; set; } = string.Empty;

    [JsonProperty("reviewId")]
    public string ReviewerId { get; set; } = string.Empty;

    [JsonProperty("rating")]
    public int Rating { get; set; }

    [JsonProperty("comment")]
    public string Comment { get; set; } = string.Empty;
    
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
}