using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserSqlService _service;
    private readonly IFavoriteSqlService _favoriteService;
    public UserController(IUserSqlService userService, IFavoriteSqlService favoriteService)
    {
        _service = userService;
        _favoriteService = favoriteService;
    }
    [EnableRateLimiting("QuoteCreationLimiter")]
    [HttpPost]
    public async Task<ActionResult<UserResponseContract>> CreateUserAsync([FromBody] UserRequestContract userRequest)
    {
        try
        {
            var request = await _service.CreateUserAsync(UserApiMapper.MapToDomein(userRequest));
            return CreatedAtAction("CreateUser", UserApiMapper.MapToResponse(request));
        }
        catch (ArgumentException)
        {
            return BadRequest("Email Already Exists");
        }
    }
    [Authorize("ReadPolicy")]
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
    [Authorize("AdminOnly")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseContract>>> GetAllUsers()
    {

        var userResponses = await _service.GetAllUsers();
        List<UserResponseContract> users = new();

        foreach (UserModel user in userResponses)
            users.Add(UserApiMapper.MapToResponse(user));

        return users;

    }
    [Authorize("AuthenticatedUser")]
    [HttpPut("{id}")]
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
    [Authorize("AdminOnly")]
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
    [Authorize("AdminOnly")]
    [HttpGet("search")] // Admin to search on a specific users first-, lastname
    public async Task<ActionResult<UserResponseContract>> GetAllUsersByNameAsync([FromQuery] string? firstName, [FromQuery] string? lastName)
    {
        var response = await _service.GetAllUsersByNameAsync(firstName, lastName);
        List<UserResponseContract> results = new();

        foreach (UserModel user in response)
            results.Add(UserApiMapper.MapToResponse(user));

        return Ok(results);
    }

    [Authorize("AuthenticatedUser")]
    [HttpPost("{userId}/favorites")]
    public async Task<ActionResult<FavoriteReponseContract>> CreateFavoriteAsync([FromRoute] Guid userId, [FromBody] Guid listingId)
    {
        var response = await _favoriteService.CreateFavoriteAsync(userId, listingId);

        return Ok(FavoriteApiMapper.MapToContract(response));
    }

    [Authorize("AuthenticatedUser")]
    [HttpGet("{userId}/favorites")]
    public async Task<ActionResult<FavoritesReponseContract>> GetAllFavoritesByUserIdAsync([FromRoute] Guid userId)
    {
        IEnumerable<FavoriteModel> response = await _favoriteService.GetAllFavoritesByUserIdAsync(userId);

        return Ok(FavoriteApiMapper.MapToContract(userId, response));
    }

    [Authorize("AuthenticatedUser")]
    [HttpDelete("{userId}/favorites/{listingId}")]
    public async Task<ActionResult> DeleteFavoriteByListingIdAsync([FromRoute] Guid userId, [FromRoute] Guid listingId)
    {
        await _favoriteService.DeleteFavoriteByListingIdAsync(userId, listingId);

        return NoContent(); // 204;
    }
    [Authorize("AuthenticatedUser")]
    [HttpGet("{userId}/favorites/{listingId}")]
    public async Task<ActionResult<bool>> IsFavoritedAsync([FromRoute] Guid userId, [FromRoute] Guid listingId)
    {
        bool isFavorite = await _favoriteService.IsFavoritedAsync(userId, listingId);

        return Ok(isFavorite);
    }
    [Authorize("AdminOnly")]
    [HttpPatch("{userId}/role")]
    public async Task<ActionResult<UserResponseContract>> UpdateUserRoleAsync([FromRoute] Guid userId, [FromBody] bool isAdmin)
    {
        var response = await _service.UpdateUserRoleAsync(userId, isAdmin);

        return Ok(UserApiMapper.MapToContract(response));
    }

    // WIP
    // [HttpGet("{userId}/favorites")]
    // public async Task<ActionResult<IEnumerable<FavoriteReponseContract>>> GetAllFavorites([FromRoute] Guid userId)
    // {

    // }
}