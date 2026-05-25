using System.Text.Json;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepo;
    private readonly HttpClient _client;
    public CarService(ICarRepository carRepository, HttpClient httpClient)
    {
        _carRepo = carRepository;
        _client = httpClient;
    }
    public async Task<CarModel> CreateCarAsync(CarModel carModel)
    {
        carModel.Id = Guid.NewGuid();
        var response = await _carRepo.CreateCarAsync(CarMapper.MapFromDomein(carModel));

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

    public async Task<IEnumerable<CarModel>> GetAllCarsByFilterAsync(CarFilterModel filter)
    {
        var response = await _carRepo.GetAllCarsByFilterAsync(CarFilterMapper.MapFromDomein(filter));
        List<CarModel> cars = new();

        foreach (CarDataModel car in response)
            cars.Add(CarMapper.MapToDomein(car));

        return cars;
    }

    public async Task<CarModel> GetCarByIdAsync(Guid carId)
    {
        var result = await _carRepo.GetCarByIdAsync(carId.ToString());
        return CarMapper.MapToDomein(result);
    }

    public async Task DeleteCarByIdAsync(Guid carId)
    {
        await _carRepo.DeleteCarByIdAsync(carId.ToString());
    }

    public async Task<CarModel> UpdateCarAsync(CarModel updateCar)
    {
        var response = await _carRepo.UpdateCarAsync(CarMapper.MapFromDomein(updateCar));
        return CarMapper.MapToDomein(response);
    }
    // This is from an external API, using this as a test case
    public async Task<IEnumerable<CarMakesModel>> GetAllCarMakes()
    {
        var response = await _client.GetAsync($"GetAllMakes?format=json");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<NhtsaMakes>(json);
            return deserialized?.Results!;
        }
        return null;
    }
}