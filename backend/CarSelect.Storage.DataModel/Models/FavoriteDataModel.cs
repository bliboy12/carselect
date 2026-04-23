
using Newtonsoft.Json;

public class FavoriteDataModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("userId")]
    public string UserId { get; set; } = string.Empty;
    
    [JsonProperty("listingId")]
    public string ListingId { get; set; } = string.Empty;
}