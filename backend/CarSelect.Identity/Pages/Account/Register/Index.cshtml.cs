using CarSelect.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarSelect.Identity.Pages.Register;

[AllowAnonymous]
public class Index : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public Index(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult OnGet(string? returnUrl)
    {
        Input = new InputModel { ReturnUrl = returnUrl };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (Input.Button != "register")
            return Redirect("~/");

        if (!ModelState.IsValid)
            return Page();

        var user = new ApplicationUser
        {
            UserName = Input.Email,
            Email = Input.Email,
            EmailConfirmed = true,
            FirstName = Input.FirstName!,
            LastName = Input.LastName!,
            CreatedAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, Input.Password!);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return Page();
        }

        // assign default User role
        await _userManager.AddToRoleAsync(user, "User");

        // sign in immediately after registration
        await _signInManager.SignInAsync(user, isPersistent: false);

        // redirect back to frontend
        if (!string.IsNullOrEmpty(Input.ReturnUrl))
            return Redirect(Input.ReturnUrl);

        return Redirect("https://pg3alicarselect.z6.web.core.windows.net/");
    }
}