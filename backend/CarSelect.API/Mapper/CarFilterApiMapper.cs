public class CarFilterApiMapper
{
    public static CarFilter MapToDomein(CarFilterRequestContract carFilterRequest)
    {
        return new CarFilter
        {
            Brand = carFilterRequest.Brand,
            Model = carFilterRequest.Model,
            Color = carFilterRequest.Color,
            Trim = carFilterRequest.Trim,
            BuildYear = carFilterRequest.BuildYear,
            Fuel = carFilterRequest.Fuel,
            Transmission = carFilterRequest.Transmission,
            MinKilometers = carFilterRequest.MinKilometers,
            MaxKilometers = carFilterRequest.MaxKilometers,
            Doors = carFilterRequest.Doors,
            Drive = carFilterRequest.Drive
        };
    }
}