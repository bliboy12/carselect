using Microsoft.EntityFrameworkCore;

public class CarSelectDbContext : DbContext
{
    public CarSelectDbContext(DbContextOptions<CarSelectDbContext> options) : base(options) { }

    public DbSet<UserDataModelSQL> Users { get; set; }
    public DbSet<FavoriteDataModelSQL> Favorites { get; set; }
    public DbSet<ReviewDataModelSQL> Reviews { get; set; }
    public DbSet<TransactionDataModelSQL> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<UserDataModelSQL>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.RegisterDate).IsRequired();
            entity.Property(u => u.CreatedAt).IsRequired();
            entity.Property(u => u.UpdatedAt).IsRequired();
        });

        // Favorite is a composite primary key
        modelBuilder.Entity<FavoriteDataModelSQL>(entity =>
        {
            entity.HasKey(f => new { f.UserId, f.ListingId });

            // real FK to User
            entity.HasOne(f => f.User)
                  .WithMany()
                  .HasForeignKey(f => f.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // ListingId is a soft reference
            entity.Property(f => f.ListingId).IsRequired();
        });

        // Review
        modelBuilder.Entity<ReviewDataModelSQL>(entity =>
        {
            entity.HasKey(r => new { r.SellerId, r.ReviewerId });

            entity.HasOne(r => r.Seller)
                .WithMany()
                .HasForeignKey(r => r.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Reviewer)
                .WithMany()
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(r => r.Rating).IsRequired();
            entity.Property(r => r.Comment).HasMaxLength(1000);
            entity.Property(r => r.CreatedAt).IsRequired();
        });

        // Transaction
        modelBuilder.Entity<TransactionDataModelSQL>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.HasOne(t => t.Buyer)
                  .WithMany()
                  .HasForeignKey(t => t.BuyerId)
                  .OnDelete(DeleteBehavior.Restrict);

            // ListingId is a soft reference
            entity.Property(t => t.ListingId).IsRequired();
            entity.Property(t => t.AgreedPrice).IsRequired().HasPrecision(18, 2);
            entity.Property(t => t.Status).IsRequired().HasMaxLength(50);
        });
    }
}