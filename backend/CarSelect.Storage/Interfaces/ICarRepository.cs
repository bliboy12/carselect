public interface ICarRepository
{
    Task<CarDataModel> CreateCarAsync(CarDataModel carDataModel);
    Task<CarDataModel> GetCarWithIdAsync(string carId);
    Task<IEnumerable<CarDataModel>> GetAllCarsAsync(); // admin use only
    //Task<IEnumerable<CarDataModel>> GetAllCarsWithOwnerIdAsync(int ownerId);
    Task<IEnumerable<CarDataModel>> GetAllCarsByFilterAsync(CarFilterDataModel filter);
    Task<CarDataModel> UpdateCarAsync(CarDataModel updateCar);
    Task RemoveCarAsync(string carId);
}