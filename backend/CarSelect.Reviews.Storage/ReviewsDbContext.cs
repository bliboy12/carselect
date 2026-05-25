using Microsoft.EntityFrameworkCore;

public class ReviewsDbContext : DbContext
{
    public ReviewsDbContext(DbContextOptions<ReviewsDbContext> options) : base(options) { }

    public DbSet<ReviewDataModelSQL> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReviewDataModelSQL>(entity =>
        {
            entity.HasKey(r => new { r.SellerId, r.ReviewerId });
            entity.Property(r => r.Rating).IsRequired();
            entity.Property(r => r.Comment).HasMaxLength(1000);
            entity.Property(r => r.CreatedAt).IsRequired();
        });
    }
}