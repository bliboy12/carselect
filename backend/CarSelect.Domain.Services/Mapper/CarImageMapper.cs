public class CarImageMapper
{
    public CarImageModel MapToDomein(CarImageDataModel carImageDataModel)
    {
        return new CarImageModel
        {
            Id = Guid.Parse(carImageDataModel.Id),
            ListingId = Guid.Parse(carImageDataModel.ListingId),
            ImageUrl = carImageDataModel.ImageUrl
        };
    }

    public CarImageDataModel MapFromDomein(CarImageModel carImageModel)
    {
        return new CarImageDataModel
        {
            Id = carImageModel.Id.ToString(),
            ListingId = carImageModel.ListingId.ToString(),
            ImageUrl = carImageModel.ImageUrl
        };
    }
}