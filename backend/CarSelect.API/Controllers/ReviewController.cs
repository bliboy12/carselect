using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ReviewController
{
    [HttpGet("reviewId")]
    public ReviewResponseContract GetReviewWithId([FromRoute] int reviewId)
    {
        throw new NotImplementedException();
    }
    [HttpPut("reviewId")]
    public ReviewResponseContract updateReview([FromRoute] int reviewId, [FromBody] ReviewRequestContract updateReviewRequest)
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    public ReviewResponseContract CreateReview([FromBody] ReviewRequestContract reviewRequest)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("reviewId")]
    public void RemoveReview([FromRoute] int reviewId)
    {
        throw new NotImplementedException();
    }
}