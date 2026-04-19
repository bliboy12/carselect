public class CarImageApiMapper
{
    public static CarImageResponseContract MapToContract(CarImageModel carImageModel)
    {
        return new CarImageResponseContract
        {
            Id = carImageModel.Id,
            ListingId = carImageModel.ListingId,
            ImageUrl = carImageModel.ImageUrl
        };
    }
    public static CarImageModel MapToDomein(CarImageResponseContract carImageResponseContract)
    {
        return new CarImageModel
        {
            Id = carImageResponseContract.Id,
            ListingId = carImageResponseContract.ListingId,
            ImageUrl = carImageResponseContract.ImageUrl
        };
    }
    public static CarImageModel MapToDomein(CarImageRequestContract carImageRequestContract)
    {
        return new CarImageModel
        {
            ImageUrl = carImageRequestContract.ImageUrl,
            IsMainImage = carImageRequestContract.IsMainImage
        };
    }
}