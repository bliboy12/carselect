public interface ICarImageService
{
    Task<CarImageModel> CreateCarImage(Guid listingId, Stream imageStream, string fileName, string contentType);
    Task<CarImageModel> GetCarImageById(Guid carImageId);
    Task<IEnumerable<CarImageModel>> GetAllCarImagesByListingIdAsync(Guid listingId);
    Task DeleteCarImageById(Guid carImageId);
}