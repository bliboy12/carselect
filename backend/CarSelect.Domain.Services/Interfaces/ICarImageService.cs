public interface ICarImageService
{
    Task<CarImageModel> CreateCarImageAsync(Guid listingId, Stream imageStream, string fileName, string contentType);
    Task<CarImageModel> GetCarImageByIdAsync(Guid listingId, Guid carImageId);
    Task<IEnumerable<CarImageModel>> GetAllCarImagesByListingIdAsync(Guid listingId);
    Task DeleteCarImageByIdAsync(Guid listingId, Guid carImageId);
    Task DeleteAllImagesByListingIdAsync(Guid listingId);
}