using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly ICarService _service;
    public CarController(ICarService carService)
    {
        _service = carService;
    }
    [HttpGet("makes")]
    public async Task<ActionResult<IEnumerable<CarMakesResponseContract>>> GetAllMakes()
    {
        var response = await _service.GetAllCarMakes();
        List<CarMakesResponseContract> carMakes = response.Select((c) => c.MapToContract()).ToList();
        return carMakes;
    }
    [Authorize("AdminPolicy")]
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
            var result = await _service.GetCarByIdAsync(id);
            return Ok(CarApiMapper.MapToContract(result));
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<CarResponseContract>>> GetAllCarsByFilterAsync([FromQuery] CarFilterRequestContract carFilterRequest)
    {
        var results = await _service.GetAllCarsByFilterAsync(CarFilterApiMapper.MapToDomein(carFilterRequest));
        List<CarResponseContract> cars = new();

        foreach (CarModel car in results)
            cars.Add(CarApiMapper.MapToContract(car));
        return Ok(cars);
    }
    // TODO: Needs to be reconsidered, a car shouldn't be created without a listing. Listing already implements this.
    // Perhaps a future feature, so for now only admin can have access
    [Authorize("AdminPolicy")]
    [HttpPost]
    public async Task<ActionResult<CarResponseContract>> CreateCarAsync([FromBody] CarRequestContract carRequest)
    {
        var result = await _service.CreateCarAsync(CarApiMapper.MapToDomein(carRequest));
        return CreatedAtAction("CreatedCar", CarApiMapper.MapToContract(result));
    }

    [Authorize("WritePolicy")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CarResponseContract>> UpdateCarAsync([FromRoute] Guid id, [FromBody] CarRequestContract updateCarRequest)
    {
        var convertToDomein = CarApiMapper.MapToDomein(updateCarRequest);
        convertToDomein.Id = id;
        var response = await _service.UpdateCarAsync(convertToDomein);

        return Ok(CarApiMapper.MapToContract(response));
    }

    // Car can't have a endpoint to delete on its own. If a car needs to be deleted, you'll have to delete the listing associated with it.
    // So only in listing can a car+listing be deleted.
    // ONLY  FOR DEVELOPMENT !!!!!
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteCarByIdAsync([FromRoute] Guid id)
    // {
    //     await _service.DeleteCarByIdAsync(id);
    //     return Ok();
    // }
}