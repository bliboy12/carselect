public class CarImageRequestContract
{
    public required IFormFile File { get; set; }
    public bool IsMainImage { get; set; } = false;
}