using System.Text.Json.Serialization;

public class PurchaseDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BuyerId { get; set; }
    public string ListingId { get; set; }
    public decimal AgreedPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}