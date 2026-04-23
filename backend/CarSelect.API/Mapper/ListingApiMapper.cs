public class ListingApiMapper
{
    public static ListingResponseContract MapToContract(ListingModel listingModel)
    {
        return new ListingResponseContract
        {
            Id = listingModel.Id,
            SellerId = listingModel.SellerId,
            CarId = listingModel.CarId,
            Price = listingModel.Price,
            CreatedAt = listingModel.CreatedAt,
            UpdatedAt = listingModel.UpdatedAt,
            Status = listingModel.Status.ToString(),
        };
    }
    public static ListingModel MapToDomein(ListingResponseContract listingResponse)
    {
        return new ListingModel
        {
            Id = listingResponse.Id,
            SellerId = listingResponse.SellerId,
            CarId = listingResponse.CarId,
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