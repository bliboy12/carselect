public interface ICarRepository
{
    CarDataModel AddCar(CarDataModel carDataModel);
    CarDataModel GetCarWithId(int carId);
    ICollection<CarDataModel> GetAllCars(); // Don't see a scenario where we need this...
    ICollection<CarDataModel> GetAllCarsWithOwnerId(int ownerId);

    // still need so more gets but with filters

    CarDataModel UpdateCar(int carId);
    void RemoveCar(int carId);
}