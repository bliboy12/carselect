
using Newtonsoft.Json;

public class ListingDataModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("sellerId")]
    public string SellerId { get; set; } = string.Empty;

    [JsonProperty("carId")]
    public string CarId { get; set; } = string.Empty;

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

}