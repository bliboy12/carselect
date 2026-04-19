using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService userService)
    {
        _service = userService;
    }
    [HttpPost]
    public async Task<ActionResult<UserResponseContract>> CreateUserAsync([FromBody] UserRequestContract userRequest)
    {
        var request = await _service.AddUserAsync(UserApiMapper.MapToDomein(userRequest));
        return UserApiMapper.MapToResponse(request);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseContract>> GetUserByIdAsync([FromRoute] Guid id)
    {
        try
        {
            var userResponse = await _service.GetUserByIdAsync(id);

            return UserApiMapper.MapToResponse(userResponse);
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseContract>>> GetAllUsers()
    {

        var userResponses = await _service.GetAllUsers();
        List<UserResponseContract> users = new();

        foreach (UserModel user in userResponses)
            users.Add(UserApiMapper.MapToResponse(user));

        return users;

    }
    [HttpPut("{id}")] // Needs 2 possibilities, one updatable params for a normal user and one for a admin (to be able to make the user an Admin)
    public async Task<ActionResult<UserResponseContract>> UpdateUserAsync([FromRoute] Guid id, [FromBody] UserRequestContract updateUser)
    {
        try
        {
            var userModel = UserApiMapper.MapToDomein(updateUser);
            userModel.Id = id;
            var userFound = await _service.UpdateUserAsync(userModel);

            return Ok(UserApiMapper.MapToResponse(userFound));
        }
        catch (NotFoundException efx)
        {
            return NotFound(efx.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveUserAsync([FromRoute] Guid id)
    {
        try
        {
            await _service.RemoveUserAsync(id);
            return Ok();
        }
        catch (NotFoundException efx)
        {
            return NotFound(efx.Message);
        }
    }
    [HttpGet("search")] // Admin to search on a specific users first-, lastname
    public async Task<ActionResult<UserResponseContract>> GetAllUsersByNameAsync([FromQuery] string firstName, [FromQuery] string lastName)
    {
        var response = await _service.GetAllUsersByNameAsync(firstName, lastName);
        List<UserResponseContract> results = new();

        foreach (UserModel user in response)
            results.Add(UserApiMapper.MapToResponse(user));

        return Ok(results);
    }
}