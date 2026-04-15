
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("car")]
public class CarController
{
    public CarResponseContract[] GetAllCars()
    {
        throw new NotImplementedException();
    }
    [HttpGet("Id")]
    public CarResponseContract GetCarWithId([FromRoute] int Id)
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
    [HttpPut("Id")]
    public void UpdateCar([FromRoute] int Id, [FromBody] CarRequestContract updateCarRequest)
    {
        throw new NotImplementedException();
    }
}