using System.Net.Http.Json;
using Duende.IdentityModel.Client;
public class ReviewAggregatorService : IReviewAggregatorService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;

    public ReviewAggregatorService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ReviewsService");
        _httpClientFactory = httpClientFactory;
    }

    private async Task SetBearerTokenAsync()
    {
        var client = _httpClientFactory.CreateClient();

        // bypass SSL in development
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        var devClient = new HttpClient(handler);

        var disco = await devClient.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await devClient.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "carselect-api-client",
                ClientSecret = "carselect-api-secret",
                Scope = "carselect.api.read"
            });

        _httpClient.SetBearerToken(tokenResponse.AccessToken!);
    }
    public async Task<IEnumerable<ReviewModel>> GetReviewsBySellerIdAsync(Guid sellerId)
    {
        await SetBearerTokenAsync();

        var reviews = await _httpClient.GetFromJsonAsync<IEnumerable<ReviewDto>>(
            $"api/reviews?sellerId={sellerId}");

        if (reviews == null) return Enumerable.Empty<ReviewModel>();

        return reviews.Select(r => new ReviewModel
        {
            SellerId = r.SellerId,
            ReviewerId = r.ReviewerId,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt
        });
    }

    public async Task<ReviewModel> CreateReviewAsync(ReviewModel review)
    {
        await SetBearerTokenAsync();

        var response = await _httpClient.PostAsJsonAsync("api/reviews", new ReviewDto
        {
            SellerId = review.SellerId,
            ReviewerId = review.ReviewerId,
            Rating = review.Rating,
            Comment = review.Comment
        });

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ReviewDto>();
        if (result == null)
            throw new Exception("Failed to create review");

        return new ReviewModel
        {
            SellerId = result.SellerId,
            ReviewerId = result.ReviewerId,
            Rating = result.Rating,
            Comment = result.Comment,
            CreatedAt = result.CreatedAt
        };
    }
}