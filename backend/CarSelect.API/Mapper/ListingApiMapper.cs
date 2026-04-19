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
            ListedDate = listingModel.ListedDate,
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
            ListedDate = listingResponse.ListedDate,
            Status = listingResponse.Status == "active" ? ListingStatus.Active : (listingResponse.Status == "sold" ? ListingStatus.Sold : ListingStatus.Removed)
        };
    }
    public static ListingModel MapToDomein(ListingRequestContract listingRequest)
    {
        return new ListingModel
        {
            SellerId = listingRequest.SellerId,
            CarId = listingRequest.CarId,
            Price = listingRequest.Price,
            ListedDate = DateTime.Now,
            Status = ListingStatus.Active,
            CarImages = listingRequest.CarImages.Select(c => CarImageApiMapper.MapToDomein(c)).ToList()
        };
    }
}