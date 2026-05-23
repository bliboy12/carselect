using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewSqlService _service;
    public ReviewController(IReviewSqlService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<ActionResult<ReviewResponseContract>> GetReviewsAsync([FromQuery] Guid? sellerId, [FromQuery] Guid? reviewerId)
    {
        if (sellerId.HasValue)
        {
            var results = await _service.GetAllReviewsBySellerIdAsync(sellerId.Value);
            return Ok(results.Select((r) => r.MapToContract()));
        }
        if (reviewerId.HasValue)
        {
            var results = await _service.GetAllReviewsByReviewerIdAsync(reviewerId.Value);
            return Ok(results.Select((r) => r.MapToContract()));
        }
        return BadRequest("Please provide with either sellerId or reviewerId");
    }

    [HttpPut]
    public async Task<ActionResult<ReviewResponseContract>> UpdateReviewByIdAsync([FromQuery] Guid reviewerId, [FromQuery] Guid sellerId, [FromBody] ReviewRequestContract newReview)
    {
        var result = await _service.UpdateReviewByIdAsync(reviewerId, sellerId, newReview.MapToDomain());
        return Ok(result.MapToContract());
    }
    [HttpPost]
    public async Task<ActionResult<ReviewResponseContract>> CreateReviewAsync([FromBody] ReviewRequestContract reviewRequest)
    {
        var newReview = await _service.CreateReviewAsync(reviewRequest.MapToDomain());
        return newReview.MapToContract();
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteReviewAsync([FromQuery] Guid reviewerId, [FromQuery] Guid sellerId)
    {
        await _service.DeleteReviewByIdAsync(reviewerId, sellerId);
        return NoContent();
    }
}