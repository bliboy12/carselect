using System.Text.Json.Serialization;

public class CarDataModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    //public int OwnerId { get; set; }
    //public UserDataModel Owner { get; set; } = null!;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Trim { get; set; } = string.Empty;
    public int BuildYear { get; set; }
    public string Fuel { get; set; } = string.Empty;
    public string Transmission { get; set; } = string.Empty;
    public int Kilometers { get; set; }
    public int Doors { get; set; }
    public string Drive { get; set; } = string.Empty;
}