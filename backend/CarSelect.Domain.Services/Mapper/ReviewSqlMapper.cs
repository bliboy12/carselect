public static class ReviewSqlMapper
{
    public static ReviewModel MapToDomein(this ReviewDataModelSQL reviewDataModelSQL)
    {
        return new ReviewModel
        {
            SellerId = reviewDataModelSQL.SellerId,
            ReviewerId = reviewDataModelSQL.ReviewerId,
            Rating = reviewDataModelSQL.Rating,
            Comment = reviewDataModelSQL.Comment,
            CreatedAt = reviewDataModelSQL.CreatedAt
        };
    }
    public static ReviewDataModelSQL MapFromDomein(this ReviewModel reviewModel)
    {
        return new ReviewDataModelSQL
        {
            SellerId = reviewModel.SellerId,
            ReviewerId = reviewModel.ReviewerId,
            Rating = reviewModel.Rating,
            Comment = reviewModel.Comment,
            CreatedAt = reviewModel.CreatedAt
        };
    }
}