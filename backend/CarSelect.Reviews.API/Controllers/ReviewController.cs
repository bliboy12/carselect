using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewController(IReviewService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(policy: "ReadPolicy")]
    public async Task<ActionResult<IEnumerable<ReviewResponseContract>>> GetReviewsAsync([FromQuery] Guid? sellerId, [FromQuery] Guid? reviewerId)
    {
        if (sellerId.HasValue)
        {
            var results = await _service.GetAllReviewsBySellerIdAsync(sellerId.Value);
            return Ok(results.Select(ReviewApiMapper.ToContract));
        }
        if (reviewerId.HasValue)
        {
            var results = await _service.GetAllReviewsByReviewerIdAsync(reviewerId.Value);
            return Ok(results.Select(ReviewApiMapper.ToContract));
        }
        return BadRequest("Please provide either sellerId or reviewerId");
    }

    [HttpGet("{sellerId}/{reviewerId}")]
    [Authorize(policy: "ReadPolicy")]
    public async Task<ActionResult<ReviewResponseContract>> GetReviewAsync([FromRoute] Guid sellerId, [FromRoute] Guid reviewerId)
    {
        var result = await _service.GetReviewByIdAsync(sellerId, reviewerId);
        if (result == null)
            return NotFound();
        return Ok(ReviewApiMapper.ToContract(result));
    }

    [HttpPost]
    [Authorize(policy: "WritePolicy")]
    public async Task<ActionResult<ReviewResponseContract>> CreateReviewAsync([FromBody] ReviewRequestContract request)
    {
        var review = await _service.CreateReviewAsync(ReviewApiMapper.ToDomain(request));
        return Ok(ReviewApiMapper.ToContract(review));
    }

    [HttpPut("{sellerId}/{reviewerId}")]
    [Authorize(policy: "WritePolicy")]
    public async Task<ActionResult<ReviewResponseContract>> UpdateReviewAsync([FromRoute] Guid sellerId, [FromRoute] Guid reviewerId, [FromBody] ReviewRequestContract request)
    {
        var result = await _service.UpdateReviewAsync(sellerId, reviewerId, ReviewApiMapper.ToDomain(request));
        return Ok(ReviewApiMapper.ToContract(result));
    }

    [HttpDelete("{sellerId}/{reviewerId}")]
    [Authorize(policy: "WritePolicy")]
    public async Task<ActionResult> DeleteReviewAsync([FromRoute] Guid sellerId, [FromRoute] Guid reviewerId)
    {
        await _service.DeleteReviewAsync(sellerId, reviewerId);
        return NoContent();
    }
}