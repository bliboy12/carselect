public static class ReviewMapper
{
    public static ReviewModel MapToDomein(this ReviewDataModel reviewDataModel)
    {
        return new ReviewModel
        {
            Id = Guid.Parse(reviewDataModel.Id),
            SellerId = Guid.Parse(reviewDataModel.SellerId),
            ReviewerId = Guid.Parse(reviewDataModel.ReviewerId),
            Rating = reviewDataModel.Rating,
            Comment = reviewDataModel.Comment,
            CreatedAt = reviewDataModel.CreatedAt
        };
    }
    public static ReviewDataModel MapFromDomein(this ReviewModel reviewModel)
    {
        return new ReviewDataModel
        {
            Id = reviewModel.Id.ToString(),
            SellerId = reviewModel.SellerId.ToString(),
            ReviewerId = reviewModel.ReviewerId.ToString(),
            Rating = reviewModel.Rating,
            Comment = reviewModel.Comment,
            CreatedAt = reviewModel.CreatedAt
        };
    }
}