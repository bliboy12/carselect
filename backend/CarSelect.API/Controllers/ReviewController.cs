using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewSqlService _service;
    public ReviewController(IReviewSqlService service)
    {
        _service = service;
    }
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<ReviewResponseContract?>> GetReviewWithIdAsync([FromRoute] Guid reviewId)
    {
        var result = await _service.GetReviewByIdAsync(reviewId);
        if (result == null)
            throw new NotFoundException($"Review with Id {reviewId} Not Found");
        return Ok(result.MapToContract());
    }
    [HttpPut("{reviewId}")]
    public async Task<ActionResult<ReviewResponseContract>> updateReviewByIdAsync([FromRoute] Guid reviewId, [FromBody] ReviewRequestContract newReview)
    {
        var result = await _service.UpdateReviewByIdAsync(reviewId, newReview.MapToDomain());
        return Ok(result.MapToContract());
    }
    [HttpPost]
    public async Task<ActionResult<ReviewResponseContract>> CreateReviewAsync([FromBody] ReviewRequestContract reviewRequest)
    {
        var newReview = await _service.CreateReviewAsync(reviewRequest.MapToDomain());
        return CreatedAtAction(nameof(GetReviewWithIdAsync), newReview.MapToContract());
    }
    [HttpDelete("{reviewId}")]
    public async Task<ActionResult> DeleteReviewAsync([FromRoute] Guid reviewId)
    {
        await _service.DeleteReviewByIdAsync(reviewId);
        return NoContent();
    }
}