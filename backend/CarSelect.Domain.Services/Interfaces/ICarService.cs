public interface ICarService
{
    Task<CarModel> CreateCarAsync(CarModel carDataModel);
    Task<CarModel> GetCarByIdAsync(Guid carId);
    Task<IEnumerable<CarModel>> GetAllCarsAsync(); // admin use only
    //Task<IEnumerable<CarDataModel>> GetAllCarsWithOwnerIdAsync(int ownerId);
    Task<IEnumerable<CarModel>> GetAllCarsByFilterAsync(CarFilterModel filter);
    Task<CarModel> UpdateCarAsync(CarModel updateCar);
    Task DeleteCarByIdAsync(Guid carId);
    Task<IEnumerable<CarMakesModel>> GetAllCarMakes();
}