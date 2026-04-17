public class PurchaseService : IPurchaseService
{
    public Task<PurchaseModel> AddPurchaseAsync(PurchaseModel purchaseDataModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PurchaseModel>> GetAllPurchasesByBuyerIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<PurchaseModel?> GetPurchaseByIdAsync(Guid purchaseId)
    {
        throw new NotImplementedException();
    }

    public Task<PurchaseModel?> GetPurchaseByListingIdAsync(Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemovePurchaseByIdAsync(Guid purchaseId)
    {
        throw new NotImplementedException();
    }

    public Task<PurchaseModel> UpdatePurchaseAsync(PurchaseModel purchaseDataModel)
    {
        throw new NotImplementedException();
    }
}