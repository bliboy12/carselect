public class CarFilterModel
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public string? Trim { get; set; }
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public FuelType? Fuel { get; set; }
    public TransmissionType? Transmission { get; set; }
    public int? MinKilometers { get; set; }
    public int? MaxKilometers { get; set; }
    public int? Doors { get; set; }
    public DriveType? Drive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}