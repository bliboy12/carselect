public class CarFilterMapper
{
    // Mappers might not match because of lowercase/uppercase mismatch in the enums
    public static CarFilterModel MapToDomein(CarFilterDataModel carFilterDataModel)
    {
        return new CarFilterModel
        {
            Brand = carFilterDataModel.Brand,
            Model = carFilterDataModel.Model,
            Color = carFilterDataModel.Color,
            Trim = carFilterDataModel.Trim,
            YearFrom = carFilterDataModel.YearFrom,
            YearTo = carFilterDataModel.YearTo,
            Fuel = carFilterDataModel.Fuel?.ToLower() == "petrol" ? FuelType.Petrol : (carFilterDataModel.Fuel?.ToLower() == "diesel" ? FuelType.diesel : (carFilterDataModel.Fuel?.ToLower() == "electric" ? FuelType.electric : FuelType.hybrid)),
            Transmission = carFilterDataModel.Transmission == "manuel" ? TransmissionType.Manuel : TransmissionType.Automatic,
            MinKilometers = carFilterDataModel.MinKilometers,
            MaxKilometers = carFilterDataModel.MaxKilometers,
            Doors = carFilterDataModel.Doors,
            Drive = carFilterDataModel.Drive?.ToLower() == "fourwd" ? DriveType.FourWd : (carFilterDataModel.Drive?.ToLower() == "fwd" ? DriveType.Fwd : (carFilterDataModel.Drive?.ToLower() == "rwd" ? DriveType.Rwd : DriveType.Awd))
        };
    }
    public static CarFilterDataModel MapFromDomein(CarFilterModel carFilterModel)
    {
        return new CarFilterDataModel
        {
            Brand = carFilterModel.Brand,
            Model = carFilterModel.Model,
            Color = carFilterModel.Color,
            Trim = carFilterModel.Trim,
            YearFrom = carFilterModel.YearFrom,
            YearTo = carFilterModel.YearTo,
            Fuel = carFilterModel.Fuel.ToString(),
            Transmission = carFilterModel.Transmission.ToString(),
            MinKilometers = carFilterModel.MinKilometers,
            MaxKilometers = carFilterModel.MaxKilometers,
            Doors = carFilterModel.Doors,
            Drive = carFilterModel.Drive.ToString()
        };
    }
}