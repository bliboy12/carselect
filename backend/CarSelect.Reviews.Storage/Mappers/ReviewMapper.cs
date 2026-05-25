public static class ReviewMapper
{
    public static ReviewModel ToDomain(ReviewDataModelSQL dataModel)
    {
        return new ReviewModel
        {
            SellerId = dataModel.SellerId,
            ReviewerId = dataModel.ReviewerId,
            Rating = dataModel.Rating,
            Comment = dataModel.Comment,
            CreatedAt = dataModel.CreatedAt
        };
    }

    public static ReviewDataModelSQL ToDataModel(ReviewModel model)
    {
        return new ReviewDataModelSQL
        {
            SellerId = model.SellerId,
            ReviewerId = model.ReviewerId,
            Rating = model.Rating,
            Comment = model.Comment,
            CreatedAt = model.CreatedAt
        };
    }
}