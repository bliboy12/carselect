public interface ICarRepository
{
    Task<CarDataModel> AddCarAsync(CarDataModel carDataModel);
    Task<CarDataModel?> GetCarWithIdAsync(string carId);
    Task<IEnumerable<CarDataModel>> GetAllCarsAsync(); // admin use only
    //Task<IEnumerable<CarDataModel>> GetAllCarsWithOwnerIdAsync(int ownerId);
    Task<IEnumerable<CarDataModel>> GetAllCarsByFilterAsync(CarFilter filter);
    Task<CarDataModel> UpdateCarAsync(CarDataModel updateCar);
    Task RemoveCarAsync(string carId);
}