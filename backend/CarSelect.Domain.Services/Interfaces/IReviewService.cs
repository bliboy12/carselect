public interface IReviewService
{
    Task<ReviewModel> AddReviewAsync(ReviewModel reviewDataModel);
    Task<ReviewModel?> GetReviewByIdAsync(Guid reviewId);
    Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId);
    Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId);
    Task<ReviewModel> UpdateReviewByIdAsync(Guid reviewId);
    Task RemoveReviewByIdAsync(Guid reviewId);
}