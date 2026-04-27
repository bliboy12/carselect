public class ListingApiMapper
{
    public static ListingResponseContract MapToContract(ListingModel listingModel)
    {
        return new ListingResponseContract
        {
            Id = listingModel.Id,
            Seller = UserApiMapper.MapToContract(listingModel.Seller),
            Car = CarApiMapper.MapToContract(listingModel.Car),
            Price = listingModel.Price,
            CreatedAt = listingModel.CreatedAt,
            UpdatedAt = listingModel.UpdatedAt,
            Status = listingModel.Status.ToString(),
            CarImages = listingModel.CarImages.Select(c => CarImageApiMapper.MapToContract(c))
        };
    }
    public static ListingModel MapToDomein(ListingResponseContract listingResponse)
    {
        return new ListingModel
        {
            Id = listingResponse.Id,
            SellerId = listingResponse.Seller.Id,
            CarId = listingResponse.Car.Id,
            Price = listingResponse.Price,
            CreatedAt = listingResponse.CreatedAt,
            UpdatedAt = listingResponse.UpdatedAt,
            Status = listingResponse.Status.ToLower() == "active" ? ListingStatus.Active : (listingResponse.Status.ToLower() == "sold" ? ListingStatus.Sold : ListingStatus.Removed)
        };
    }
    public static ListingModel MapToDomein(ListingRequestContract listingRequest)
    {
        return new ListingModel
        {
            SellerId = listingRequest.SellerId,
            CarId = listingRequest.CarId,
            Price = listingRequest.Price,
            Status = (ListingStatus)listingRequest.Status,
            CarImages = listingRequest.CarImages.Select(c => CarImageApiMapper.MapToDomein(c)).ToList()
        };
    }
    public static ListingModel MapToDomein(CreateListingRequestContract listingRequest)
    {
        return new ListingModel
        {
            SellerId = listingRequest.SellerId,

            Car = CarApiMapper.MapToDomein(listingRequest.Car),
            Price = listingRequest.Price,
            Status = (ListingStatus)listingRequest.Status,
            CarImages = listingRequest.CarImages.Select(c => CarImageApiMapper.MapToDomein(c)).ToList()
        };
    }
}