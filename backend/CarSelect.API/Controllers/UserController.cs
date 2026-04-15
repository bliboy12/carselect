using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
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