using Newtonsoft.Json;

public class UserDataModel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonProperty("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("password")]
    public string Password { get; set; } = string.Empty;

    [JsonProperty("registerDate")]
    public DateTime RegisterDate { get; set; }

    [JsonProperty("isAdmin")]
    public bool IsAdmin { get; set; } = false;

    [JsonProperty("isDeleted")]
    public bool IsDeleted { get; set; } = false;
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}