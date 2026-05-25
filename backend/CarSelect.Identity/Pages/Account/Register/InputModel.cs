using System.ComponentModel.DataAnnotations;

namespace CarSelect.Identity.Pages.Register;

public class InputModel
{
    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(8)]
    public string? Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    public string? ReturnUrl { get; set; }
    public string? Button { get; set; }
}