public class ReviewService : IReviewService
{
    public Task<ReviewModel> AddReviewAsync(ReviewModel reviewDataModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewModel?> GetReviewByIdAsync(Guid reviewId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveReviewByIdAsync(Guid reviewId)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewModel> UpdateReviewByIdAsync(Guid reviewId)
    {
        throw new NotImplementedException();
    }
}