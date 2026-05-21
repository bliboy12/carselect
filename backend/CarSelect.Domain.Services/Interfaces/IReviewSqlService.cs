public interface IReviewSqlService
{
    Task<ReviewModel> CreateReviewAsync(ReviewModel reviewModel);

    Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId);
    Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId);

    Task<ReviewModel?> GetReviewByIdAsync(Guid reviewId);

    Task DeleteReviewByIdAsync(Guid reviewId);

    Task<ReviewModel> UpdateReviewByIdAsync(Guid reviewId, ReviewModel newReview);
}