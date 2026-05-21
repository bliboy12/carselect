public class ReviewSqlService : IReviewSqlService
{
    private readonly IReviewSqlRepository _repo;

    public ReviewSqlService(IReviewSqlRepository repo)
    {
        _repo = repo;
    }

    public async Task<ReviewModel> CreateReviewAsync(ReviewModel reviewModel)
    {
        ReviewDataModelSQL result = await _repo.CreateReviewAsync(ReviewSqlMapper.MapFromDomein(reviewModel));
        return ReviewSqlMapper.MapToDomein(result);
    }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId)
    {
        IEnumerable<ReviewDataModelSQL> results = await _repo.GetAllReviewsByReviewerIdAsync(reviewerId);
        return results.Select(r => ReviewSqlMapper.MapToDomein(r)).ToList();
    }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId)
    {
        IEnumerable<ReviewDataModelSQL> results = await _repo.GetAllReviewsBySellerIdAsync(sellerId);
        return results.Select(r => ReviewSqlMapper.MapToDomein(r)).ToList();
    }

    public async Task<ReviewModel?> GetReviewByIdAsync(Guid reviewId)
    {
        ReviewDataModelSQL? result = await _repo.GetReviewByIdAsync(reviewId);
        return result == null ? null : ReviewSqlMapper.MapToDomein(result);
    }

    public async Task DeleteReviewByIdAsync(Guid reviewId)
    {
        await _repo.DeleteReviewByIdAsync(reviewId);
    }

    public async Task<ReviewModel> UpdateReviewByIdAsync(Guid reviewId, ReviewModel newReview)
    {
        ReviewDataModelSQL result = await _repo.UpdateReviewByIdAsync(reviewId, ReviewSqlMapper.MapFromDomein(newReview));
        return ReviewSqlMapper.MapToDomein(result);
    }
}