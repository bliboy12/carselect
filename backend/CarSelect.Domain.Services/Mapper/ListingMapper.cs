public class ListingMapper
{
    public ListingModel MapToDomein(ListingDataModel listingDataModel)
    {
        return new ListingModel
        {
            Id = Guid.Parse(listingDataModel.Id),
            SellerId = Guid.Parse(listingDataModel.SellerId),
            CarId = Guid.Parse(listingDataModel.CarId),
            Price = listingDataModel.Price,
            ListedDate = listingDataModel.ListedDate,
            Status = listingDataModel.Status == "active" ? ListingStatus.Active : (listingDataModel.Status == "sold" ? ListingStatus.Sold : ListingStatus.Removed)
        };
    }
    public ListingDataModel MapFromDomein(ListingModel listingModel)
    {
        return new ListingDataModel
        {
            Id = listingModel.Id.ToString(),
            SellerId = listingModel.SellerId.ToString(),
            CarId = listingModel.CarId.ToString(),
            Price = listingModel.Price,
            ListedDate = listingModel.ListedDate,
            Status = listingModel.Status.ToString()
        };
    }
}