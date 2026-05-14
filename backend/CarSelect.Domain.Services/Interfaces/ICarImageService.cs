public interface ICarImageService
{
    Task<CarImageModel> CreateCarImageAsync(Guid listingId, Stream imageStream, string fileName, string contentType, bool isMainImage);
    Task<CarImageModel> GetCarImageByIdAsync(Guid listingId, Guid carImageId);
    Task<IEnumerable<CarImageModel>> GetAllCarImagesByListingIdAsync(Guid listingId);
    Task<IEnumerable<CarImageModel>> GetMainImagesOfAllListingsAsync();
    Task<CarImageModel> SetMainImageAsync(Guid listingId, Guid carImageId);
    Task DeleteCarImageByIdAsync(Guid listingId, Guid carImageId);
    Task DeleteAllImagesByListingIdAsync(Guid listingId);
}