using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [HttpGet("Id")]
    public async Task<ActionResult<UserResponseContract>> GetUser([FromRoute] int Id)
    {
        try
        {

        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public UserResponseContract[] GetAllUsers()
    {
        throw new NotImplementedException();
    }
    [HttpPut("{id}")] // Needs 2 possibilities, one updatable params for a normal user and one for a admin (to be able to make the user an Admin)
    public void UpdateUser([FromRoute] int id, [FromBody] UserRequestContract updateUser)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("{id}")]
    public void RemoveUser([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
    [HttpGet] // Admin to search on a specific users first-, lastname
    public UserResponseContract[] GetUsersName([FromQuery] string query)
    {
        throw new NotImplementedException();
    }
}