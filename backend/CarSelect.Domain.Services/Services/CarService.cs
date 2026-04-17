public class CarService : ICarService
{
    public Task<CarModel> AddCarAsync(CarModel carDataModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CarModel>> GetAllCarsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CarModel>> GetAllCarsByFilterAsync(CarFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<CarModel?> GetCarWithIdAsync(Guid carId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCarAsync(Guid carId)
    {
        throw new NotImplementedException();
    }

    public Task<CarModel> UpdateCarAsync(CarModel updateCar)
    {
        throw new NotImplementedException();
    }
}