public interface ITransactionRepository
{
    Task<TransactionDataModel> AddTransactionAsync(TransactionDataModel TransactionDataModel);
    Task<TransactionDataModel?> GetTransactionByIdAsync(string transactionId);
    Task<IEnumerable<TransactionDataModel>> GetAllTransactionsByBuyerIdAsync(string userId);
    Task<TransactionDataModel?> GetTransactionByListingIdAsync(string listingId);
    Task<TransactionDataModel> UpdateTransactionAsync(TransactionDataModel transactionDataModel);
    Task RemoveTransactionByIdAsync(string transactionId);
}