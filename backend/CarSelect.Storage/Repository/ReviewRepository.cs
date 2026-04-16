public class ReviewRepository : IReviewRepository
{
    public Task<ReviewDataModel> AddReviewAsync(ReviewDataModel reviewDataModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReviewDataModel>> GetAllReviewsByReviewerIdAsync(int reviewerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReviewDataModel>> GetAllReviewsBySellerIdAsync(int sellerId)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewDataModel?> GetReviewByIdAsync(int reviewId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveReviewByIdAsync(int reviewId)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewDataModel> UpdateReviewByIdAsync(ReviewDataModel reviewId)
    {
        throw new NotImplementedException();
    }
}