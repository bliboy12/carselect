public interface IBlobStorageRepository
{
    Task<string> UploadImageAsync(Stream stream, string fileName, string contentType);
    Task DeleteImageAsync(string imageUrl);
}