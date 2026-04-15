
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/car")]
public class CarController
{
    public CarResponseContract[] GetAllCars()
    {
        throw new NotImplementedException();
    }
    [HttpGet("{id}")]
    public CarResponseContract GetCarWithId([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
    [HttpGet]
    public CarResponseContract[] GetAllFilteredCars([FromQuery] string query)
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    public CarResponseContract CreateCar([FromBody] CarRequestContract carRequest)
    {
        throw new NotImplementedException();
    }
    [HttpPut("{id}")]
    public void UpdateCar([FromRoute] int id, [FromBody] CarRequestContract updateCarRequest)
    {
        throw new NotImplementedException();
    }
}