public interface IReviewRepository
{
    Task<ReviewModel> CreateReviewAsync(ReviewModel review);
    Task<ReviewModel?> GetReviewByIdAsync(Guid sellerId, Guid reviewerId);
    Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId);
    Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId);
    Task<ReviewModel> UpdateReviewAsync(Guid sellerId, Guid reviewerId, ReviewModel review);
    Task DeleteReviewAsync(Guid sellerId, Guid reviewerId);
}