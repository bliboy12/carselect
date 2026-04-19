using System.Text.Json.Serialization;

public class PurchaseDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public string ListingId { get; set; } = string.Empty;
    public decimal AgreedPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}