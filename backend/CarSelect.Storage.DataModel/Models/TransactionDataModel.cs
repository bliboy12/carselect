
using Newtonsoft.Json;

public class TransactionDataModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("buyerId")]
    public string BuyerId { get; set; } = string.Empty;

    [JsonProperty("listingId")]
    public string ListingId { get; set; } = string.Empty;

    [JsonProperty("agreedPrice")]
    public decimal AgreedPrice { get; set; }

    [JsonProperty("transactionDate")]
    public DateTime TransactionDate { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
}