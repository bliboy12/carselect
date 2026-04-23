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
            Fuel = (FuelTypeContract)carModel.Fuel,
            Transmission = (TransmissionTypeContract)carModel.Transmission,
            Kilometers = carModel.Kilometers,
            Doors = carModel.Doors,
            Drive = (DriveTypeContract)carModel.Drive
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
            Fuel = (FuelType)carResponseContract.Fuel,
            Transmission = (TransmissionType)carResponseContract.Transmission,
            Kilometers = carResponseContract.Kilometers,
            Doors = carResponseContract.Doors,
            Drive = (DriveType)carResponseContract.Drive
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
            Fuel = (FuelType)carResponseContract.Fuel,
            Transmission = (TransmissionType)carResponseContract.Transmission,
            Kilometers = carResponseContract.Kilometers,
            Doors = carResponseContract.Doors,
            Drive = (DriveType)carResponseContract.Drive
        };
    }
}