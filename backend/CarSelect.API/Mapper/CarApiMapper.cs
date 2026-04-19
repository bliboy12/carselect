public class CarApiMapper
{
    public static CarResponseContract MapToContract(CarModel carModel)
    {
        return new CarResponseContract
        {
            Id = carModel.Id,
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
    public static CarModel MapToDomein(CarResponseContract carResponseContract)
    {
        return new CarModel
        {
            Id = carResponseContract.Id,
            Brand = carResponseContract.Brand,
            Model = carResponseContract.Model,
            Color = carResponseContract.Color,
            Trim = carResponseContract.Trim,
            BuildYear = carResponseContract.BuildYear,
            Fuel = carResponseContract.Fuel.ToLower() == "petrol" ? FuelType.Petrol : (carResponseContract.Fuel.ToLower() == "diesel" ? FuelType.diesel : (carResponseContract.Fuel.ToLower() == "electric" ? FuelType.electric : FuelType.hybrid)),
            Transmission = carResponseContract.Transmission.ToLower() == "automatic" ? TransmissionType.Automatic : TransmissionType.Manuel,
            Kilometers = carResponseContract.Kilometers,
            Doors = carResponseContract.Doors,
            Drive = carResponseContract.Drive.ToLower() == "4wd" ? DriveType.FourWd : (carResponseContract.Drive.ToLower() == "fwd" ? DriveType.Fwd : (carResponseContract.Drive.ToLower() == "rwd" ? DriveType.Rwd : DriveType.Awd))
        };
    }
    public static CarModel MapToDomein(CarRequestContract carResponseContract)
    {
        return new CarModel
        {
            Brand = carResponseContract.Brand,
            Model = carResponseContract.Model,
            Color = carResponseContract.Color,
            Trim = carResponseContract.Trim,
            BuildYear = carResponseContract.BuildYear,
            Fuel = carResponseContract.Fuel.ToLower() == "petrol" ? FuelType.Petrol : (carResponseContract.Fuel.ToLower() == "diesel" ? FuelType.diesel : (carResponseContract.Fuel.ToLower() == "electric" ? FuelType.electric : FuelType.hybrid)),
            Transmission = carResponseContract.Transmission.ToLower() == "automatic" ? TransmissionType.Automatic : TransmissionType.Manuel,
            Kilometers = carResponseContract.Kilometers,
            Doors = carResponseContract.Doors,
            Drive = carResponseContract.Drive.ToLower() == "4wd" ? DriveType.FourWd : (carResponseContract.Drive.ToLower() == "fwd" ? DriveType.Fwd : (carResponseContract.Drive.ToLower() == "rwd" ? DriveType.Rwd : DriveType.Awd))
        };
    }
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