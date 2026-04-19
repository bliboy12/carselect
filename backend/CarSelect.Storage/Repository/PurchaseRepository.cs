public class PurchaseRepository : IPurchaseRepository
{
    public Task<PurchaseDataModel> AddPurchaseAsync(PurchaseDataModel purchaseDataModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PurchaseDataModel>> GetAllPurchasesByBuyerIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<PurchaseDataModel?> GetPurchaseByIdAsync(int purchaseId)
    {
        throw new NotImplementedException();
    }

    public Task<PurchaseDataModel?> GetPurchaseByListingIdAsync(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemovePurchaseByIdAsync(int purchaseId)
    {
        throw new NotImplementedException();
    }

    public Task<PurchaseDataModel> UpdatePurchaseAsync(PurchaseDataModel purchaseDataModel)
    {
        throw new NotImplementedException();
    }
}