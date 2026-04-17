public interface ICarService
{
    Task<CarModel> AddCarAsync(CarModel carDataModel);
    Task<CarModel?> GetCarWithIdAsync(Guid carId);
    Task<IEnumerable<CarModel>> GetAllCarsAsync(); // admin use only
    //Task<IEnumerable<CarDataModel>> GetAllCarsWithOwnerIdAsync(int ownerId);
    Task<IEnumerable<CarModel>> GetAllCarsByFilterAsync(CarFilter filter);
    Task<CarModel> UpdateCarAsync(CarModel updateCar);
    Task RemoveCarAsync(Guid carId);
}