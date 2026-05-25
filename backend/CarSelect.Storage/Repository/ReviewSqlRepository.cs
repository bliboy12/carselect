using Microsoft.EntityFrameworkCore;

public class ReviewSqlRepository : IReviewSqlRepository
{
    private readonly CarSelectDbContext _context;

    public ReviewSqlRepository(CarSelectDbContext context)
    {
        _context = context;
    }

    public async Task<ReviewDataModelSQL> CreateReviewAsync(ReviewDataModelSQL review)
    {

        review.CreatedAt = DateTime.UtcNow;

        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<ReviewDataModelSQL?> GetReviewByIdAsync(Guid reviewId, Guid sellerId)
    {
        return await _context.Reviews.FindAsync(reviewId, sellerId);
    }

    public async Task<IEnumerable<ReviewDataModelSQL>> GetAllReviewsByReviewerIdAsync(Guid reviewerId)
    {
        return await _context.Reviews
            .Where(r => r.ReviewerId == reviewerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ReviewDataModelSQL>> GetAllReviewsBySellerIdAsync(Guid sellerId)
    {
        return await _context.Reviews
            .Where(r => r.SellerId == sellerId)
            .ToListAsync();
    }

    public async Task DeleteReviewByIdAsync(Guid reviewId, Guid sellerId)
    {
        var review = await GetReviewByIdAsync(reviewId, sellerId);
        if (review == null)
            throw new NotFoundException($"Review with id {reviewId} Not Found");

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }

    public async Task<ReviewDataModelSQL> UpdateReviewByIdAsync(Guid reviewId, Guid sellerId, ReviewDataModelSQL newReview)
    {
        var existing = await GetReviewByIdAsync(reviewId, sellerId);
        if (existing == null)
            throw new NotFoundException($"Review with id {reviewId} Not Found");

        existing.Rating = newReview.Rating;
        existing.Comment = newReview.Comment;

        _context.Reviews.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }
}