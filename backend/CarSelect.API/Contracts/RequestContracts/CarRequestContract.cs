public class CarRequestContract
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Trim { get; set; }
    public int BuildYear { get; set; }
    public string Fuel { get; set; }
    public string Transmission { get; set; }
    public int Kilometers { get; set; }
    public int Doors { get; set; }
    public DriveType Drive { get; set; }
}