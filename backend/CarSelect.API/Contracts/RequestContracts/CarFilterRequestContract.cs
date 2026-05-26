public class CarFilterRequestContract
{
    private string _brand = string.Empty;
    public string? Brand
    {
        get => _brand;
        init => _brand = value?.Trim().ToLower();
    }
    private string _model = string.Empty;
    public string? Model
    {
        get => _model;
        init => _model = value?.Trim().ToLower();
    }
    private string _color = string.Empty;
    public string? Color
    {
        get => _color;
        init => _color = value?.Trim().ToLower();
    }
    private string _trim = string.Empty;
    public string? Trim
    {
        get => _trim;
        init => _trim = value?.Trim().ToLower();
    }
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public FuelTypeContract? Fuel { get; set; }
    public TransmissionTypeContract? Transmission { get; set; }
    public int? MinKilometers { get; set; }
    public int? MaxKilometers { get; set; }
    public int? Doors { get; set; }
    public DriveTypeContract? Drive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}