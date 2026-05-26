using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reviews")]
public class ReviewAggregatorController : ControllerBase
{
    private readonly IReviewAggregatorService _reviewAggregatorService;

    public ReviewAggregatorController(IReviewAggregatorService reviewAggregatorService)
    {
        _reviewAggregatorService = reviewAggregatorService;
    }

    [HttpGet]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<ActionResult<IEnumerable<EnrichedReviewResponseContract>>> GetReviewsBySellerIdAsync([FromQuery] Guid sellerId)
    {
        try
        {
            var results = await _reviewAggregatorService.GetReviewsBySellerIdAsync(sellerId);
            return Ok(results.Select(ReviewAggregatorMapper.ToContract));
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }

    [HttpPost]
    [Authorize("WritePolicy")]
    public async Task<ActionResult<EnrichedReviewResponseContract>> CreateReviewAsync([FromBody] ReviewRequestContract request)
    {
        try
        {
            var reviewModel = new ReviewModel
            {
                SellerId = request.SellerId,
                ReviewerId = request.ReviewerId,
                Rating = request.Rating,
                Comment = request.Comment
            };
            var result = await _reviewAggregatorService.CreateReviewAsync(reviewModel);
            return Ok(ReviewAggregatorMapper.ToContract(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}