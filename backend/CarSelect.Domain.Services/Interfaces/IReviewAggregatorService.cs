public interface IReviewAggregatorService
{
    Task<IEnumerable<ReviewModel>> GetReviewsBySellerIdAsync(Guid sellerId);
    Task<ReviewModel> CreateReviewAsync(ReviewModel reviewModel);
}