public class TransactionSqlService : ITransactionSqlService
{
    private readonly ITransactionSqlRepository _repo;

    public TransactionSqlService(ITransactionSqlRepository repo)
    {
        _repo = repo;
    }

    public async Task<TransactionModel> AddTransactionAsync(TransactionModel transaction)
    {
        var dataModel = new TransactionDataModelSQL
        {
            Id = Guid.NewGuid(),
            BuyerId = transaction.BuyerId,
            ListingId = transaction.ListingId,
            AgreedPrice = transaction.AgreedPrice,
            TransactionDate = DateTime.UtcNow,
            Status = transaction.Status
        };
        var result = await _repo.AddTransactionAsync(dataModel);
        return MapToModel(result);
    }

    public async Task<TransactionModel?> GetTransactionsByListingIdAsync(Guid listingId)
    {
        var result = await _repo.GetTransactionByListingIdAsync(listingId);
        return result == null ? null : MapToModel(result);
    }

    public async Task<IEnumerable<TransactionModel>> GetAllTransactionsByBuyerIdAsync(Guid userId)
    {
        var results = await _repo.GetAllTransactionsByBuyerIdAsync(userId);
        return results.Select(MapToModel);
    }

    public Task<TransactionModel?> GetTransactionByIdAsync(Guid transactionId)
        => throw new NotImplementedException();

    public Task<TransactionModel> UpdateTransactionAsync(TransactionModel transactionDataModel)
        => throw new NotImplementedException();

    public Task RemoveTransactionByIdAsync(Guid transactionId)
        => throw new NotImplementedException();

    private TransactionModel MapToModel(TransactionDataModelSQL dataModel)
    {
        return new TransactionModel
        {
            Id = dataModel.Id,
            BuyerId = dataModel.BuyerId,
            ListingId = dataModel.ListingId,
            AgreedPrice = dataModel.AgreedPrice,
            TransactionDate = dataModel.TransactionDate,
            Status = dataModel.Status
        };
    }
}