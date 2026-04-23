public class ListingMapper
{
    public static ListingModel MapToDomein(ListingDataModel listingDataModel)
    {
        return new ListingModel
        {
            Id = Guid.Parse(listingDataModel.Id),
            SellerId = Guid.Parse(listingDataModel.SellerId),
            CarId = Guid.Parse(listingDataModel.CarId),
            Price = listingDataModel.Price,
            CreatedAt = listingDataModel.CreatedAt,
            UpdatedAt = listingDataModel.UpdatedAt,
            Status = listingDataModel.Status.ToLower() == "active" ? ListingStatus.Active : (listingDataModel.Status.ToLower() == "sold" ? ListingStatus.Sold : ListingStatus.Removed)
        };
    }
    public static ListingDataModel MapFromDomein(ListingModel listingModel)
    {
        return new ListingDataModel
        {
            Id = listingModel.Id.ToString(),
            SellerId = listingModel.SellerId.ToString(),
            CarId = listingModel.CarId.ToString(),
            Price = listingModel.Price,
            CreatedAt = listingModel.CreatedAt,
            UpdatedAt = listingModel.UpdatedAt,
            Status = listingModel.Status.ToString()
        };
    }
}