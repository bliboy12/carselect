using Microsoft.Azure.Cosmos;

public class CarImageService : ICarImageService
{
    private readonly ICarImageRepository _carImageRepo;
    private readonly IBlobStorageRepository _blobStorageRepo;
    public CarImageService(ICarImageRepository carImageRepository, IBlobStorageRepository blobStorageRepository)
    {
        _carImageRepo = carImageRepository;
        _blobStorageRepo = blobStorageRepository;
    }
    public async Task<CarImageModel> CreateCarImageAsync(Guid listingId, Stream imageStream, string fileName, string contentType)
    {
        var existingImages = await _carImageRepo.GetAllCarImagesByListingIdAsync(listingId.ToString());
        bool imageExists = false;
        foreach (CarImageDataModel carImage in existingImages)
        {
            if (carImage.ImageUrl.Contains(fileName))
                imageExists = true;
        }

        if (imageExists)
            throw new Exception($"Image With FileName {fileName} Already Exists");

        var imageUrl = await _blobStorageRepo.UploadImageAsync(imageStream, fileName, contentType);

        var carImageModel = new CarImageModel
        {
            Id = Guid.NewGuid(),
            ListingId = listingId,
            ImageUrl = imageUrl,
            IsMainImage = false
        };
        // chekcs to see if this is the first image to be added for this listingID
        carImageModel.IsMainImage = !existingImages.Any();

        try
        {
            var result = await _carImageRepo.CreateCarImageAsync(CarImageMapper.MapFromDomein(carImageModel));
            return CarImageMapper.MapToDomein(result);
        }
        catch (Exception)
        {
            // Cosmos failed to upload the CarImage so we remove the image that has been stored in the BLOB
            // Otherwise we will have a stranded Blob Image that has no reference in our project
            await _blobStorageRepo.DeleteImageAsync(imageUrl);
            throw;
        }
    }

    public async Task<CarImageModel> GetCarImageByIdAsync(Guid listingId, Guid carImageId)
    {
        var carImage = await _carImageRepo.GetCarImageByIdAsync(listingId.ToString(), carImageId.ToString());
        return CarImageMapper.MapToDomein(carImage);
    }

    public async Task<IEnumerable<CarImageModel>> GetAllCarImagesByListingIdAsync(Guid listingId)
    {
        var carImages = await _carImageRepo.GetAllCarImagesByListingIdAsync(listingId.ToString());
        List<CarImageModel> results = new();

        foreach (CarImageDataModel car in carImages)
            results.Add(CarImageMapper.MapToDomein(car));

        return results;
    }

    public async Task DeleteCarImageByIdAsync(Guid listingId, Guid carImageId)
    {
        // 1) we get the the Car Image object to able to find the URL we need to delete from the blob
        var carImage = await _carImageRepo.GetCarImageByIdAsync(listingId.ToString(), carImageId.ToString());
        if (carImage is null)
            throw new NotFoundException($"carImage with Id {carImageId.ToString()} Not Found");

        // 2) If we found it, this will execute which will cause the blob storage of this image to be deleted
        await _blobStorageRepo.DeleteImageAsync(carImage.ImageUrl);
        // 3) afterwards we delete the Car Image that we retrieved aswell, finally performing full delete in both Blob and Cosmos
        await _carImageRepo.DeleteCarImageByIdAsync(listingId.ToString(), carImageId.ToString());
    }

    public async Task DeleteAllImagesByListingIdAsync(Guid listingId)
    {
        IEnumerable<CarImageModel> getAllImages = await GetAllCarImagesByListingIdAsync(listingId);

        foreach (CarImageModel carImage in getAllImages)
        {
            await _blobStorageRepo.DeleteImageAsync(carImage.ImageUrl);
            await _carImageRepo.DeleteCarImageByIdAsync(listingId.ToString(), carImage.Id.ToString());
        }
    }
}