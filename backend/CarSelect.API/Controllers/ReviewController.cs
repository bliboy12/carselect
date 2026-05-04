using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reviews")]
public class ReviewController
{
    private readonly IReviewService _service;
    public ReviewController(IReviewService service)
    {
        _service = service;
    }
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<ReviewResponseContract?>> GetReviewWithIdAsync([FromRoute] Guid reviewId)
    {
        var result = await _service.GetReviewByIdAsync(reviewId);
        if (result == null)
            throw new NotFoundException($"Review with Id {reviewId} Not Found");
        return result.MapToContract();
    }
    [HttpPut("{reviewId}")]
    public async Task<ReviewResponseContract> updateReviewByIdAsync([FromRoute] Guid reviewId, [FromBody] ReviewRequestContract newReview)
    {
        var result = await _service.UpdateReviewByIdAsync(reviewId, newReview.MapToDomain());
        return result.MapToContract();
    }
    [HttpPost]
    public async Task<ReviewResponseContract> CreateReviewAsync([FromBody] ReviewRequestContract reviewRequest)
    {
        var newReview = await _service.CreateReviewAsync(reviewRequest.MapToDomain());
        return newReview.MapToContract();
    }
    [HttpDelete("{reviewId}")]
    public async Task DeleteReviewAsync([FromRoute] Guid reviewId)
    {
        await _service.DeleteReviewByIdAsync(reviewId);
    }
}