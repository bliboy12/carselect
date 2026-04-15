public class CarRepository : ICarRepository
{
    public CarDataModel AddCar(CarDataModel carDataModel)
    {
        throw new NotImplementedException();
    }

    public ICollection<CarDataModel> GetAllCars()
    {
        throw new NotImplementedException();
    }

    public ICollection<CarDataModel> GetAllCarsWithOwnerId(int ownerId)
    {
        throw new NotImplementedException();
    }

    public CarDataModel GetCarWithId(int carId)
    {
        throw new NotImplementedException();
    }

    public void RemoveCar(int carId)
    {
        throw new NotImplementedException();
    }

    public CarDataModel UpdateCar(int carId)
    {
        throw new NotImplementedException();
    }
}