public class CarImageMapper
{
    public static CarImageModel MapToDomein(CarImageDataModel carImageDataModel)
    {
        return new CarImageModel
        {
            Id = Guid.Parse(carImageDataModel.Id),
            ListingId = Guid.Parse(carImageDataModel.ListingId),
            ImageUrl = carImageDataModel.ImageUrl,
            IsMainImage = carImageDataModel.IsMainImage
        };
    }

    public static CarImageDataModel MapFromDomein(CarImageModel carImageModel)
    {
        return new CarImageDataModel
        {
            Id = carImageModel.Id.ToString(),
            ListingId = carImageModel.ListingId.ToString(),
            ImageUrl = carImageModel.ImageUrl,
            IsMainImage = carImageModel.IsMainImage
        };
    }
}