using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using CarSelect.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Duende.IdentityServer.Extensions;

public class CarSelectProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CarSelectProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject)
            ?? throw new Exception("Cannot map roles for (null) user!");
        var roles = await _userManager.GetRolesAsync(user);
        var claims = roles.Select(role => new Claim("role", role));
        context.IssuedClaims.AddRange(claims);

        context.IssuedClaims.Add(new Claim("given_name", user.FirstName));
        context.IssuedClaims.Add(new Claim("family_name", user.LastName));
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var existing = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
        context.IsActive = existing?.IsDeleted == false;
    }
}