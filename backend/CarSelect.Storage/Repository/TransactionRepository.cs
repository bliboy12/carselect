public class TransactionRepository : ITransactionRepository
{
    public Task<TransactionDataModel> AddTransactionAsync(TransactionDataModel TransactionDataModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TransactionDataModel>> GetAllTransactionsByBuyerIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionDataModel?> GetTransactionByIdAsync(string transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionDataModel?> GetTransactionByListingIdAsync(string listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTransactionByIdAsync(string transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionDataModel> UpdateTransactionAsync(TransactionDataModel transactionDataModel)
    {
        throw new NotImplementedException();
    }
}