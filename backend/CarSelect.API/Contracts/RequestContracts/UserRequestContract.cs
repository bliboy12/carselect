public class UserRequestContract
{
    private string _firstName = string.Empty;
    public required string FirstName
    {
        get => _firstName;
        init => _firstName = value.Trim().ToLower();
    }
    private string _lastName = string.Empty;
    public required string LastName
    {
        get => _lastName;
        init => _lastName = value.Trim().ToLower();
    }
    private string _email = string.Empty;
    public required string Email
    {
        get => _email;
        init => _email = value.Trim().ToLower();
    }
    public required string Password { get; set; }

}