public class PurchaseMapper
{
    public PurchaseModel MapToDomein(PurchaseDataModel purchaseDataModel)
    {
        return new PurchaseModel
        {
            Id = Guid.Parse(purchaseDataModel.Id),
            BuyerId = Guid.Parse(purchaseDataModel.BuyerId),
            ListingId = Guid.Parse(purchaseDataModel.ListingId),
            AgreedPrice = purchaseDataModel.AgreedPrice,
            PurchaseDate = purchaseDataModel.PurchaseDate
        };
    }

    public PurchaseDataModel MapFromDomein(PurchaseModel purchaseModel)
    {
        return new PurchaseDataModel
        {
            Id = purchaseModel.Id.ToString(),
            BuyerId = purchaseModel.BuyerId.ToString(),
            ListingId = purchaseModel.ListingId.ToString(),
            AgreedPrice = purchaseModel.AgreedPrice,
            PurchaseDate = purchaseModel.PurchaseDate
        };
    }
}