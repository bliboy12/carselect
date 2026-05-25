using Microsoft.EntityFrameworkCore;

public class ReviewSqlRepository : IReviewRepository
{
    private readonly ReviewsDbContext _context;

    public ReviewSqlRepository(ReviewsDbContext context)
    {
        _context = context;
    }

    public async Task<ReviewModel> CreateReviewAsync(ReviewModel review)
    {
        review.CreatedAt = DateTime.UtcNow;
        var dataModel = ReviewMapper.ToDataModel(review);
        await _context.Reviews.AddAsync(dataModel);
        await _context.SaveChangesAsync();
        return ReviewMapper.ToDomain(dataModel);
    }

    public async Task<ReviewModel?> GetReviewByIdAsync(Guid sellerId, Guid reviewerId)
    {
        var result = await _context.Reviews
            .FirstOrDefaultAsync(r => r.SellerId == sellerId && r.ReviewerId == reviewerId);
        return result == null ? null : ReviewMapper.ToDomain(result);
    }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId)
    {
        var results = await _context.Reviews
            .Where(r => r.SellerId == sellerId)
            .ToListAsync();
        return results.Select(ReviewMapper.ToDomain);
    }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId)
    {
        var results = await _context.Reviews
            .Where(r => r.ReviewerId == reviewerId)
            .ToListAsync();
        return results.Select(ReviewMapper.ToDomain);
    }

    public async Task<ReviewModel> UpdateReviewAsync(Guid sellerId, Guid reviewerId, ReviewModel review)
    {
        var existing = await _context.Reviews
            .FirstOrDefaultAsync(r => r.SellerId == sellerId && r.ReviewerId == reviewerId);

        if (existing == null)
            throw new Exception($"Review not found");

        existing.Rating = review.Rating;
        existing.Comment = review.Comment;

        await _context.SaveChangesAsync();
        return ReviewMapper.ToDomain(existing);
    }

    public async Task DeleteReviewAsync(Guid sellerId, Guid reviewerId)
    {
        var existing = await _context.Reviews
            .FirstOrDefaultAsync(r => r.SellerId == sellerId && r.ReviewerId == reviewerId);

        if (existing == null)
            throw new Exception($"Review not found");

        _context.Reviews.Remove(existing);
        await _context.SaveChangesAsync();
    }
}