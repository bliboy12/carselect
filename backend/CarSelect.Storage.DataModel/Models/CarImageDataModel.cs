using Newtonsoft.Json;

public class CarImageDataModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("listingId")]
    public string ListingId { get; set; } = string.Empty;

    [JsonProperty("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonProperty("isMainImage")]
    public bool IsMainImage { get; set; }
}