public interface IPurchaseService
{
    Task<PurchaseModel> AddPurchaseAsync(PurchaseModel purchaseDataModel);
    Task<PurchaseModel?> GetPurchaseByIdAsync(Guid purchaseId);
    Task<IEnumerable<PurchaseModel>> GetAllPurchasesByBuyerIdAsync(Guid userId);
    Task<PurchaseModel?> GetPurchaseByListingIdAsync(Guid listingId);
    Task<PurchaseModel> UpdatePurchaseAsync(PurchaseModel purchaseDataModel);
    Task RemovePurchaseByIdAsync(Guid purchaseId);
}