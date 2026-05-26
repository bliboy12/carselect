using Microsoft.EntityFrameworkCore;

public class CarSelectDbContext : DbContext
{
    public CarSelectDbContext(DbContextOptions<CarSelectDbContext> options) : base(options) { }

    public DbSet<FavoriteDataModelSQL> Favorites { get; set; }
    public DbSet<TransactionDataModelSQL> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Favorite is a composite primary key
        modelBuilder.Entity<FavoriteDataModelSQL>(entity =>
        {
            entity.HasKey(f => new { f.UserId, f.ListingId });

            // real FK to User
            entity.Property(u => u.UserId);

            // ListingId is a soft reference
            entity.Property(f => f.ListingId).IsRequired();
        });

        // Transaction
        modelBuilder.Entity<TransactionDataModelSQL>(entity =>
        {
            entity.HasKey(t => t.Id);

            // Soft reference user
            entity.Property(t => t.BuyerId).IsRequired();

            // ListingId is a soft reference
            entity.Property(t => t.ListingId).IsRequired();
            entity.Property(t => t.AgreedPrice).IsRequired().HasPrecision(18, 2);
            entity.Property(t => t.Status).IsRequired().HasMaxLength(50);
        });
    }
}