using System.Net.Http.Json;

public class ReviewAggregatorService : IReviewAggregatorService
{
    private readonly HttpClient _httpClient;
    private readonly IUserService _userService;

    public ReviewAggregatorService(IHttpClientFactory httpClientFactory, IUserService userService)
    {
        _httpClient = httpClientFactory.CreateClient("ReviewsService");
        _userService = userService;
    }

    public async Task<IEnumerable<ReviewModel>> GetReviewsBySellerIdAsync(Guid sellerId)
    {
        var reviews = await _httpClient.GetFromJsonAsync<IEnumerable<ReviewDto>>(
            $"api/reviews?sellerId={sellerId}");

        if (reviews == null) return Enumerable.Empty<ReviewModel>();

        var enriched = new List<ReviewModel>();

        foreach (var review in reviews)
        {
            var reviewModel = new ReviewModel
            {
                SellerId = review.SellerId,
                ReviewerId = review.ReviewerId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };

            try
            {
                var reviewer = await _userService.GetUserByIdAsync(review.ReviewerId);
                var seller = await _userService.GetUserByIdAsync(review.SellerId);
                reviewModel.Reviewer = reviewer;
                reviewModel.Seller = seller;
            }
            catch (NotFoundException nfe)
            {
                // If users ID's doesn't exist then something when wrong
                throw new NotFoundException(nfe.Message);
            }

            enriched.Add(reviewModel);
        }

        return enriched;
    }
}