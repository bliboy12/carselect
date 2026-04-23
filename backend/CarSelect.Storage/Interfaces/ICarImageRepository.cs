public interface ICarImageRepository
{
    Task<CarImageDataModel> CreateCarImageAsync(CarImageDataModel carImageModel);
    Task<CarImageDataModel> GetCarImageByIdAsync(string carImageId, string listingId);
    Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(string listingId);
    Task DeleteCarImageByIdAsync(string listingId, string carImageId);
}