public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repo;
    public ReviewService(IReviewRepository repo)
    {
        _repo = repo;
    }
    // public async Task<ReviewModel> CreateReviewAsync(ReviewModel reviewDataModel)
    // {
    //     ReviewDataModel result = await _repo.CreateReviewAsync(reviewDataModel.MapFromDomein());
    //     return result.MapToDomein();
    // }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsByReviewerIdAsync(Guid reviewerId)
    {
        IEnumerable<ReviewDataModel> results = await _repo.GetAllReviewsByReviewerIdAsync(reviewerId.ToString());
        List<ReviewModel> reviews = results.Select((r) => r.MapToDomein()).ToList();

        return reviews;
    }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsBySellerIdAsync(Guid sellerId)
    {
        IEnumerable<ReviewDataModel> results = await _repo.GetAllReviewsByReviewerIdAsync(sellerId.ToString());
        List<ReviewModel> reviews = results.Select((r) => r.MapToDomein()).ToList();

        return reviews;
    }

    public async Task<ReviewModel?> GetReviewByIdAsync(Guid reviewId)
    {
        ReviewDataModel? result = await _repo.GetReviewByIdAsync(reviewId.ToString());
        return result?.MapToDomein();
    }

    public async Task DeleteReviewByIdAsync(Guid reviewId)
    {
        await _repo.DeleteReviewByIdAsync(reviewId.ToString());
    }

    // public async Task<ReviewModel> UpdateReviewByIdAsync(Guid reviewId, ReviewModel newReview)
    // {
    //     ReviewDataModel result = await _repo.UpdateReviewByIdAsync(reviewId.ToString(), newReview.MapFromDomein());
    //     return result.MapToDomein();
    // }
}