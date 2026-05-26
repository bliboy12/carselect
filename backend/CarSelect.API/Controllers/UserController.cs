using CarSelect.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFavoriteSqlService _favoriteService;

    public UserController(UserManager<ApplicationUser> userManager, IFavoriteSqlService favoriteService)
    {
        _userManager = userManager;
        _favoriteService = favoriteService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseContract>> GetUserByIdAsync([FromRoute] Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound($"User with id {id} not found");

        var response = UserApiMapper.MapToResponse(user);
        response.IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseContract>>> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var adminIds = (await _userManager.GetUsersInRoleAsync("Admin"))
            .Select(u => u.Id)
            .ToHashSet();

        return Ok(users.Select(u =>
        {
            var response = UserApiMapper.MapToResponse(u);
            response.IsAdmin = adminIds.Contains(u.Id);
            return response;
        }));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseContract>> UpdateUserAsync([FromRoute] Guid id, [FromBody] UserRequestContract updateUser)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound($"User with id {id} not found");

        user.FirstName = updateUser.FirstName;
        user.LastName = updateUser.LastName;
        user.Email = updateUser.Email;
        user.UpdateAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        var response = UserApiMapper.MapToResponse(user);
        response.IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveUserAsync([FromRoute] Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound($"User with id {id} not found");

        user.FirstName = "Deleted";
        user.LastName = "User";
        user.Email = $"deleted_{id}@deleted.com";
        user.IsDeleted = true;
        user.UpdateAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);
        return Ok();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<UserResponseContract>>> GetAllUsersByNameAsync([FromQuery] string? firstName, [FromQuery] string? lastName)
    {
        var query = _userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(firstName))
            query = query.Where(u => u.FirstName.StartsWith(firstName));
        if (!string.IsNullOrEmpty(lastName))
            query = query.Where(u => u.LastName.StartsWith(lastName));

        var users = await query.ToListAsync();

        if (!users.Any())
            return NotFound($"No users found");

        var adminIds = (await _userManager.GetUsersInRoleAsync("Admin"))
            .Select(u => u.Id)
            .ToHashSet();

        return Ok(users.Select(u =>
        {
            var response = UserApiMapper.MapToResponse(u);
            response.IsAdmin = adminIds.Contains(u.Id);
            return response;
        }));
    }

    [HttpPatch("{id}/role")]
    public async Task<ActionResult<UserResponseContract>> UpdateUserRoleAsync([FromRoute] Guid id, [FromBody] bool isAdmin)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound($"User with id {id} not found");

        if (isAdmin)
            await _userManager.AddToRoleAsync(user, "Admin");
        else
            await _userManager.RemoveFromRoleAsync(user, "Admin");

        var response = UserApiMapper.MapToResponse(user);
        response.IsAdmin = isAdmin;
        return Ok(response);
    }

    // Favorites
    [HttpPost("{userId}/favorites")]
    public async Task<ActionResult<FavoriteReponseContract>> CreateFavoriteAsync([FromRoute] Guid userId, [FromBody] Guid listingId)
    {
        var response = await _favoriteService.CreateFavoriteAsync(userId, listingId);
        return Ok(FavoriteApiMapper.MapToContract(response));
    }

    [HttpGet("{userId}/favorites")]
    public async Task<ActionResult<FavoriteReponseContract>> GetAllFavoritesByUserIdAsync([FromRoute] Guid userId)
    {
        IEnumerable<FavoriteModel> response = await _favoriteService.GetAllFavoritesByUserIdAsync(userId);
        return Ok(FavoriteApiMapper.MapToContract(userId, response));
    }

    [HttpDelete("{userId}/favorites/{listingId}")]
    public async Task<ActionResult> DeleteFavoriteByListingIdAsync([FromRoute] Guid userId, [FromRoute] Guid listingId)
    {
        await _favoriteService.DeleteFavoriteByListingIdAsync(userId, listingId);
        return NoContent();
    }

    [HttpGet("{userId}/favorites/{listingId}")]
    public async Task<ActionResult<bool>> IsFavoritedAsync([FromRoute] Guid userId, [FromRoute] Guid listingId)
    {
        bool isFavorite = await _favoriteService.IsFavoritedAsync(userId, listingId);
        return Ok(isFavorite);
    }
}