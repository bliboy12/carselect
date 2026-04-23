using Newtonsoft.Json;

public class CarDataModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
    //public int OwnerId { get; set; }
    //public UserDataModel Owner { get; set; } = null!;
    [JsonProperty("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonProperty("model")]
    public string Model { get; set; } = string.Empty;

    [JsonProperty("color")]
    public string Color { get; set; } = string.Empty;

    [JsonProperty("trim")]
    public string Trim { get; set; } = string.Empty;

    [JsonProperty("buildYear")]
    public int BuildYear { get; set; }

    [JsonProperty("fuel")]
    public string Fuel { get; set; } = string.Empty;

    [JsonProperty("transmission")]
    public string Transmission { get; set; } = string.Empty;

    [JsonProperty("kilometers")]
    public int Kilometers { get; set; }

    [JsonProperty("doors")]
    public int Doors { get; set; }

    [JsonProperty("drive")]
    public string Drive { get; set; } = string.Empty;
}