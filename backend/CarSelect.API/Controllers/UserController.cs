using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users")]
public class UserController
{
    [HttpGet("Id")]
    public UserResponseContract GetUser([FromRoute] int Id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public UserResponseContract[] GetAllUsers()
    {
        throw new NotImplementedException();
    }
    [HttpPut("Id")] // Needs 2 possibilities, one updatable params for a normal user and one for a admin (to be able to make the user an Admin)
    public void UpdateUser([FromRoute] int Id, [FromBody] UserRequestContract updateUser)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("Id")]
    public void RemoveUser([FromRoute] int Id)
    {
        throw new NotImplementedException();
    }
    [HttpGet] // Admin to search on a specific users first-, lastname
    public UserResponseContract[] GetUsersName([FromQuery] string query)
    {
        throw new NotImplementedException();
    }
}