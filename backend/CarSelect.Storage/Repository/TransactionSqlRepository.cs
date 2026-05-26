using Microsoft.EntityFrameworkCore;

public class TransactionSqlRepository : ITransactionSqlRepository
{
    private readonly CarSelectDbContext _context;

    public TransactionSqlRepository(CarSelectDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionDataModelSQL> AddTransactionAsync(TransactionDataModelSQL transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<TransactionDataModelSQL?> GetTransactionByListingIdAsync(Guid listingId)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(t => t.ListingId == listingId);
    }

    public async Task<IEnumerable<TransactionDataModelSQL>> GetAllTransactionsByBuyerIdAsync(Guid buyerId)
    {
        return await _context.Transactions
            .Where(t => t.BuyerId == buyerId)
            .ToListAsync();
    }
}