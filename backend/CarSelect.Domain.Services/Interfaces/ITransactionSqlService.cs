public interface ITransactionSqlService
{
    Task<TransactionModel> AddTransactionAsync(TransactionModel transaction);
    Task<TransactionModel?> GetTransactionsByListingIdAsync(Guid listingId);
    Task<IEnumerable<TransactionModel>> GetAllTransactionsByBuyerIdAsync(Guid buyerId);
}