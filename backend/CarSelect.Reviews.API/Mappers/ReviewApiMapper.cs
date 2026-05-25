public static class ReviewApiMapper
{
    public static ReviewResponseContract ToContract(ReviewModel reviewModel)
    {
        return new ReviewResponseContract
        {
            SellerId = reviewModel.SellerId,
            ReviewerId = reviewModel.ReviewerId,
            Rating = reviewModel.Rating,
            Comment = reviewModel.Comment,
            CreatedAt = reviewModel.CreatedAt
        };
    }

    public static ReviewModel ToDomain(ReviewRequestContract reviewRequestContract)
    {
        return new ReviewModel
        {
            SellerId = reviewRequestContract.SellerId,
            ReviewerId = reviewRequestContract.ReviewerId,
            Rating = reviewRequestContract.Rating,
            Comment = reviewRequestContract.Comment,
        };
    }
}