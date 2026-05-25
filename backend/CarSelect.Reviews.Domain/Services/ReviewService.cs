public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repo;

    public ReviewService(IReviewRepository repo)
    {
        _repo = repo;
    }

    public async Task<ReviewModel> CreateReviewAsync(ReviewModel review)
    {
        review.CreatedAt = DateTime.UtcNow;
        return await _repo.CreateReviewAsync(review);
    }

    public async Task<ReviewModel?> GetReviewByIdAsync(Guid sellerId, Guid reviewerId)
    {
        return await _repo.GetReviewByIdAsync(sellerId, reviewerId);
    }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId)
    {
        return await _repo.GetAllReviewsBySellerIdAsync(sellerId);
    }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId)
    {
        return await _repo.GetAllReviewsByReviewerIdAsync(reviewerId);
    }

    public async Task<ReviewModel> UpdateReviewAsync(Guid sellerId, Guid reviewerId, ReviewModel review)
    {
        return await _repo.UpdateReviewAsync(sellerId, reviewerId, review);
    }

    public async Task DeleteReviewAsync(Guid sellerId, Guid reviewerId)
    {
        await _repo.DeleteReviewAsync(sellerId, reviewerId);
    }
}