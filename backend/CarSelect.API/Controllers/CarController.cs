
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/car")]
public class CarController : ControllerBase
{
    private readonly ICarService _service;
    public CarController(ICarService carService)
    {
        _service = carService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarResponseContract>>> GetAllCarsAsync()
    {
        var response = await _service.GetAllCarsAsync();
        List<CarResponseContract> cars = new();

        foreach (CarModel car in response)
            cars.Add(CarApiMapper.MapToContract(car));

        return Ok(cars);

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<CarResponseContract>> GetCarWithIdAsync([FromRoute] Guid id)
    {
        try
        {
            var result = await _service.GetCarWithIdAsync(id);
            return Ok(CarApiMapper.MapToContract(result));
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CarResponseContract>>> GetAllCarsByFilterAsync([FromQuery] CarFilterRequestContract carFilterRequest)
    {
        var results = await _service.GetAllCarsByFilterAsync(CarFilterApiMapper.MapToDomein(carFilterRequest));
        List<CarResponseContract> cars = new();

        foreach (CarModel car in results)
            cars.Add(CarApiMapper.MapToContract(car));
        return Ok(cars);
    }
    [HttpPost]
    public async Task<ActionResult<CarResponseContract>> CreateCarAsync([FromBody] CarRequestContract carRequest)
    {
        var result = await _service.CreateCarAsync(CarApiMapper.MapToDomein(carRequest));
        return Ok(CarApiMapper.MapToContract(result));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<CarResponseContract>> UpdateCarASync([FromRoute] Guid id, [FromBody] CarRequestContract updateCarRequest)
    {
        var convertToDomein = CarApiMapper.MapToDomein(updateCarRequest);
        convertToDomein.Id = id;
        var response = await _service.UpdateCarAsync(convertToDomein);

        return Ok(CarApiMapper.MapToContract(response));
    }
}