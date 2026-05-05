public static class ReivewApiMapper
{
    public static ReviewResponseContract MapToContract(this ReviewModel reviewModel)
    {
        return new ReviewResponseContract
        {
            Id = reviewModel.Id,
            SellerId = reviewModel.SellerId,
            ReviewerId = reviewModel.ReviewerId,
            Rating = reviewModel.Rating,
            Comment = reviewModel.Comment,
            CreatedAt = reviewModel.CreatedAt
        };
    }
    public static ReviewModel MapToDomain(this ReviewRequestContract reviewRequestContract)
    {
        return new ReviewModel
        {
            SellerId = reviewRequestContract.SellerId,
            ReviewerId = reviewRequestContract.ReviewerId,
            Rating = reviewRequestContract.Rating,
            Comment = reviewRequestContract.Comment,
            CreatedAt = reviewRequestContract.CreatedAt
        };
    }
}