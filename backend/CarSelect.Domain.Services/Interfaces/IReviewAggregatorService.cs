public interface IReviewAggregatorService
{
    Task<IEnumerable<ReviewModel>> GetReviewsBySellerIdAsync(Guid sellerId);
}