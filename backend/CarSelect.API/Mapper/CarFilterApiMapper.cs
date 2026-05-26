public class CarFilterApiMapper
{
    public static CarFilterModel MapToDomein(CarFilterRequestContract carFilterRequest)
    {
        return new CarFilterModel
        {
            Brand = carFilterRequest.Brand,
            Model = carFilterRequest.Model,
            Color = carFilterRequest.Color,
            Trim = carFilterRequest.Trim,
            YearFrom = carFilterRequest.YearFrom,
            YearTo = carFilterRequest.YearTo,
            Fuel = carFilterRequest.Fuel == null ? null : (FuelType)carFilterRequest.Fuel,
            Transmission = carFilterRequest.Transmission == null ? null : (TransmissionType)carFilterRequest.Transmission,
            MinKilometers = carFilterRequest.MinKilometers,
            MaxKilometers = carFilterRequest.MaxKilometers,
            Doors = carFilterRequest.Doors,
            Drive = carFilterRequest.Drive == null ? null : (DriveType)carFilterRequest.Drive,
            Page = carFilterRequest.Page,
            PageSize = carFilterRequest.PageSize
        };
    }
}