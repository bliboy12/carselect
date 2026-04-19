public interface IPurchaseRepository
{
    Task<PurchaseDataModel> AddPurchaseAsync(PurchaseDataModel purchaseDataModel);
    Task<PurchaseDataModel?> GetPurchaseByIdAsync(int purchaseId);
    Task<IEnumerable<PurchaseDataModel>> GetAllPurchasesByBuyerIdAsync(int userId);
    Task<PurchaseDataModel?> GetPurchaseByListingIdAsync(int listingId);
    Task<PurchaseDataModel> UpdatePurchaseAsync(PurchaseDataModel purchaseDataModel);
    Task RemovePurchaseByIdAsync(int purchaseId);
}