public interface ITransactionSqlRepository
{
    Task<TransactionDataModelSQL> AddTransactionAsync(TransactionDataModelSQL transaction);
    Task<TransactionDataModelSQL?> GetTransactionByListingIdAsync(Guid listingId);
    Task<IEnumerable<TransactionDataModelSQL>> GetAllTransactionsByBuyerIdAsync(Guid buyerId);
}