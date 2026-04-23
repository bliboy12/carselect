public interface IReviewRepository
{
    Task<ReviewDataModel> AddReviewAsync(ReviewDataModel reviewDataModel);
    Task<ReviewDataModel?> GetReviewByIdAsync(int reviewId);
    Task<IEnumerable<ReviewDataModel>> GetAllReviewsBySellerIdAsync(int sellerId);
    Task<IEnumerable<ReviewDataModel>> GetAllReviewsByReviewerIdAsync(int reviewerId);
    Task<ReviewDataModel> UpdateReviewByIdAsync(ReviewDataModel reviewId);
    Task RemoveReviewByIdAsync(int reviewId);
}