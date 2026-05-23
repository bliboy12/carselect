public interface IReviewSqlService
{
    Task<ReviewModel> CreateReviewAsync(ReviewModel reviewModel);

    Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId);
    Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId);

    Task<ReviewModel?> GetReviewByIdAsync(Guid reviewId, Guid sellerId);

    Task DeleteReviewByIdAsync(Guid reviewId, Guid sellerId);

    Task<ReviewModel> UpdateReviewByIdAsync(Guid reviewId, Guid sellerId, ReviewModel newReview);
}