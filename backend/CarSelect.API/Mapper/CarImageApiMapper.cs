public class CarImageApiMapper
{
    public static CarImageResponseContract MapToContract(CarImageModel carImageModel)
    {
        return new CarImageResponseContract
        {
            Id = carImageModel.Id,
            ListingId = carImageModel.ListingId,
            ImageUrl = carImageModel.ImageUrl,
            IsMainImage = carImageModel.IsMainImage
        };
    }
    public static CarImageModel MapToDomein(CarImageResponseContract carImageResponseContract)
    {
        return new CarImageModel
        {
            Id = carImageResponseContract.Id,
            ListingId = carImageResponseContract.ListingId,
            ImageUrl = carImageResponseContract.ImageUrl,
            IsMainImage = carImageResponseContract.IsMainImage
        };
    }
    public static CarImageModel MapToDomein(CarImageRequestContract carImageRequestContract)
    {
        return new CarImageModel
        {
            IsMainImage = carImageRequestContract.IsMainImage
        };
    }
}