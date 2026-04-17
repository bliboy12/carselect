public class ReviewMapper
{
    public ReviewModel MapToDomein(ReviewDataModel reviewDataModel)
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
    public ReviewDataModel MapFromDomein(ReviewModel reviewModel)
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