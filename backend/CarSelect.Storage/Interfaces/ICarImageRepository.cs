public interface ICarImageRepository
{
    Task<CarImageDataModel> CreateCarImageAsync(CarImageDataModel carImageModel);
    Task<CarImageDataModel> GetCarImageByIdAsync(string carImageId);
    Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(string listingId);
    Task DeleteCarImageAsync(string carImageId);
}