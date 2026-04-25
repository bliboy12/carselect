public interface ICarImageRepository
{
    Task<CarImageDataModel> CreateCarImageAsync(CarImageDataModel carImageModel);
    Task<CarImageDataModel> GetCarImageByIdAsync(string listingId, string carImageId);
    Task<IEnumerable<CarImageDataModel>> GetMainImagesOfAllListingsAsync();
    Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(string listingId);
    Task DeleteCarImageByIdAsync(string listingId, string carImageId);
    Task<CarImageDataModel> UpdateCarImage(CarImageDataModel carImageData);
    Task<CarImageDataModel?> GetMainImageByListingId(string listingId);
    Task<CarImageDataModel> SetMainImageAsync(CarImageDataModel newMainImage, CarImageDataModel oldMainImage);
}