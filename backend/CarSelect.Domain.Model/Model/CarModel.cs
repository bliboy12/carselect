
public class CarModel
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public UserModel Owner { get; set; } = null!;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Trim { get; set; } = string.Empty;
    public int BuildYear { get; set; }
    public FuelType Fuel { get; set; }
    public TransmissionType Transmission { get; set; }
    public int Kilometers { get; set; }
    public int Doors { get; set; }
    public DriveType Drive { get; set; }

}