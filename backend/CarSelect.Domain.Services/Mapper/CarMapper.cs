public class CarMapper
{
    public static CarModel MapToDomein(CarDataModel carDataModel)
    {
        return new CarModel
        {
            Id = Guid.Parse(carDataModel.Id),
            Brand = carDataModel.Brand,
            Model = carDataModel.Model,
            Color = carDataModel.Color,
            Trim = carDataModel.Trim,
            BuildYear = carDataModel.BuildYear,
            Fuel = carDataModel.Fuel == "Petrol" ? FuelType.Petrol : (carDataModel.Fuel == "diesel" ? FuelType.diesel : (carDataModel.Fuel == "electric" ? FuelType.electric : FuelType.hybrid)),
            Transmission = carDataModel.Transmission.ToLower() == "manuel" ? TransmissionType.Manuel : TransmissionType.Automatic,
            Kilometers = carDataModel.Kilometers,
            Doors = carDataModel.Doors,
            Drive = carDataModel.Drive.ToLower() == "4wd" ? DriveType.FourWd : (carDataModel.Drive.ToLower() == "fwd" ? DriveType.Fwd : (carDataModel.Drive.ToLower() == "rwd" ? DriveType.Rwd : DriveType.Awd))
        };
    }
    public static CarDataModel MapFromDomein(CarModel carModel)
    {
        return new CarDataModel
        {
            Id = carModel.Id.ToString(),
            Brand = carModel.Brand,
            Model = carModel.Model,
            Color = carModel.Color,
            Trim = carModel.Trim,
            BuildYear = carModel.BuildYear,
            Fuel = carModel.Fuel.ToString(),
            Transmission = carModel.Transmission.ToString(),
            Kilometers = carModel.Kilometers,
            Doors = carModel.Doors,
            Drive = carModel.Drive.ToString()
        };
    }
}