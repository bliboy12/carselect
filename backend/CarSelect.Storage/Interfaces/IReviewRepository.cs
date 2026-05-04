public interface IReviewRepository
{
    Task<ReviewDataModel> CreateReviewAsync(ReviewDataModel reviewDataModel);
    Task<ReviewDataModel?> GetReviewByIdAsync(string reviewId);
    Task<IEnumerable<ReviewDataModel>> GetAllReviewsBySellerIdAsync(string sellerId);
    Task<IEnumerable<ReviewDataModel>> GetAllReviewsByReviewerIdAsync(string reviewerId);
    Task<ReviewDataModel> UpdateReviewByIdAsync(string reviewId, ReviewDataModel newReview);
    Task DeleteReviewByIdAsync(string reviewId);
}