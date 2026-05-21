public interface IReviewSqlRepository
{
    Task<ReviewDataModelSQL> CreateReviewAsync(ReviewDataModelSQL reviewDataModel);
    Task<ReviewDataModelSQL?> GetReviewByIdAsync(Guid reviewId);
    Task<IEnumerable<ReviewDataModelSQL>> GetAllReviewsBySellerIdAsync(Guid sellerId);
    Task<IEnumerable<ReviewDataModelSQL>> GetAllReviewsByReviewerIdAsync(Guid reviewerId);
    Task<ReviewDataModelSQL> UpdateReviewByIdAsync(Guid reviewId, ReviewDataModelSQL newReview);
    Task DeleteReviewByIdAsync(Guid reviewId);
}