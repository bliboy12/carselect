public interface ITransactionService
{
    Task<TransactionModel> AddTransactionAsync(TransactionModel transactionDataModel);
    Task<TransactionModel?> GetTransactionByIdAsync(Guid transactionId);
    Task<IEnumerable<TransactionModel>> GetAllTransactionsByBuyerIdAsync(Guid userId);
    Task<TransactionModel?> GetTransactionsByListingIdAsync(Guid listingId);
    Task<TransactionModel> UpdateTransactionAsync(TransactionModel transactionDataModel);
    Task RemoveTransactionByIdAsync(Guid transactionId);
}