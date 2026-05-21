public interface IReviewSqlRepository
{
    Task<ReviewDataModelSQL> CreateReviewAsync(ReviewDataModelSQL reviewDataModel);
    Task<ReviewDataModelSQL?> GetReviewByIdAsync(Guid reviewId, Guid sellerId);
    Task<IEnumerable<ReviewDataModelSQL>> GetAllReviewsBySellerIdAsync(Guid sellerId);
    Task<IEnumerable<ReviewDataModelSQL>> GetAllReviewsByReviewerIdAsync(Guid reviewerId);
    Task<ReviewDataModelSQL> UpdateReviewByIdAsync(Guid reviewId, Guid sellerId, ReviewDataModelSQL newReview);
    Task DeleteReviewByIdAsync(Guid reviewId, Guid sellerId);
}