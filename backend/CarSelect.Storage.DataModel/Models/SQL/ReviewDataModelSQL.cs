
using Newtonsoft.Json;

public class ReviewDataModelSQL
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public UserDataModelSQL Seller { get; set; } = null!;
    public Guid ReviewerId { get; set; }
    public UserDataModelSQL Reviewer { get; set; } = null!;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}