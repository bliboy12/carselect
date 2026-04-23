public class TransactionService : ITransactionService
{
    public Task<TransactionModel> AddTransactionAsync(TransactionModel transactionDataModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TransactionModel>> GetAllTransactionsByBuyerIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionModel?> GetTransactionByIdAsync(Guid transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionModel?> GetTransactionsByListingIdAsync(Guid listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTransactionByIdAsync(Guid transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionModel> UpdateTransactionAsync(TransactionModel transactionDataModel)
    {
        throw new NotImplementedException();
    }
}