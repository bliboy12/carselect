public interface ICarImageRepository
{
    CarImageDataModel AddCarImage(CarImageDataModel carImageData);
    CarImageDataModel UpdateCarImage(CarImageDataModel carImageData);
    ICollection<CarImageDataModel> GetAllCarImagesWithListingId(int listingId);
    CarImageDataModel GetFirstCarImageFromListingId(int listingId); // TO BE SEEN
    void RemoveCarImage(int carImageId);
}