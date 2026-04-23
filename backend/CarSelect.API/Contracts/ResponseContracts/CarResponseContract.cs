public class CarResponseContract
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Trim { get; set; } = string.Empty;
    public int BuildYear { get; set; }
    public FuelTypeContract Fuel { get; set; }
    public TransmissionTypeContract Transmission { get; set; }
    public int Kilometers { get; set; }
    public int Doors { get; set; }
    public DriveTypeContract Drive { get; set; }
}