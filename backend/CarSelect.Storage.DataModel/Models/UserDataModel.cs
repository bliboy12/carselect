using Newtonsoft.Json;

public class UserDataModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime RegisterDate { get; set; }
    public bool IsAdmin { get; set; } = false;
}