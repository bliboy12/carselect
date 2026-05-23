using Microsoft.EntityFrameworkCore;

public class FavoriteSqlRepository : IFavoriteSqlRepository
{
    private readonly CarSelectDbContext _context;
    public FavoriteSqlRepository(CarSelectDbContext context)
    {
        _context = context;
    }
    // Favorites
    public async Task<FavoriteDataModelSQL> CreateFavoriteAsync(FavoriteDataModelSQL favorite)
    {
        await _context.Favorites.AddAsync(favorite);
        await _context.SaveChangesAsync();
        return favorite;
    }

    public async Task<IEnumerable<FavoriteDataModelSQL>> GetAllFavoritesByUserIdAsync(Guid userId)
    {
        return await _context.Favorites.Where(f => f.UserId == userId).ToListAsync();
    }

    public async Task<bool> IsFavoritedAsync(Guid userId, Guid listingId)
    {
        return await _context.Favorites.AnyAsync(f => f.UserId == userId && f.ListingId == listingId);
    }

    public async Task DeleteFavoriteByListingIdAsync(Guid userId, Guid listingId)
    {
        var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ListingId == listingId);

        if (favorite == null)
            throw new NotFoundException($"Favorite with listingId {listingId} Not Found");

        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();
    }
}