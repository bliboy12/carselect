public class CarRequestContract
{
    private string _brand = string.Empty;
    public required string Brand
    {
        get => _brand;
        init => _brand = value.Trim().ToLower();
    }
    private string _model = string.Empty;
    public required string Model
    {
        get => _model;
        init => _model = value.Trim().ToLower();
    }
    private string _color = string.Empty;
    public required string Color
    {
        get => _color;
        init => _color = value.Trim().ToLower();
    }
    private string _trim = string.Empty;
    public required string Trim
    {
        get => _trim;
        init => _trim = value.Trim().ToLower();
    }
    public required int BuildYear { get; set; }
    public required FuelTypeContract Fuel { get; set; }
    public required TransmissionTypeContract Transmission { get; set; }
    public required int Kilometers { get; set; }
    public required int Doors { get; set; }
    public required DriveTypeContract Drive { get; set; }
}