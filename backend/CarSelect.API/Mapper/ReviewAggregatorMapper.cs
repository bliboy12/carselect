public static class ReviewAggregatorMapper
{
    public static EnrichedReviewResponseContract ToContract(ReviewModel review)
    {
        return new EnrichedReviewResponseContract
        {
            SellerId = review.SellerId,
            ReviewerId = review.ReviewerId,
            ReviewerFirstName = review.Reviewer?.FirstName ?? "Unknown",
            ReviewerLastName = review.Reviewer?.LastName ?? "Unknown",
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt
        };
    }
}