public class CarService : ICarService
{
    private readonly ICarRepository _carRepo;
    public CarService(ICarRepository carRepository)
    {
        _carRepo = carRepository;
    }
    public async Task<CarModel> CreateCarAsync(CarModel carDataModel)
    {
        carDataModel.Id = Guid.NewGuid();
        var response = await _carRepo.CreateCarAsync(CarMapper.MapFromDomein(carDataModel));
        return CarMapper.MapToDomein(response);
    }

    public async Task<IEnumerable<CarModel>> GetAllCarsAsync()
    {
        var response = await _carRepo.GetAllCarsAsync();
        List<CarModel> cars = new();

        foreach (CarDataModel car in response)
            cars.Add(CarMapper.MapToDomein(car));
        return cars;
    }

    public async Task<IEnumerable<CarModel>> GetAllCarsByFilterAsync(CarFilter filter)
    {
        var response = await _carRepo.GetAllCarsByFilterAsync(CarMapper.MapFromDomein(filter));
        List<CarModel> cars = new();

        foreach (CarDataModel car in response)
            cars.Add(CarMapper.MapToDomein(car));

        return cars;
    }

    public async Task<CarModel> GetCarWithIdAsync(Guid carId)
    {
        var result = await _carRepo.GetCarWithIdAsync(carId.ToString());
        return CarMapper.MapToDomein(result);
    }

    public async Task RemoveCarAsync(Guid carId)
    {
        await _carRepo.RemoveCarAsync(carId.ToString());
    }

    public async Task<CarModel> UpdateCarAsync(CarModel updateCar)
    {
        var response = await _carRepo.UpdateCarAsync(CarMapper.MapFromDomein(updateCar));
        return CarMapper.MapToDomein(response);
    }
}