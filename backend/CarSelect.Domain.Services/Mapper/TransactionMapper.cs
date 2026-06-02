public class TransactionMapper
{
    public static TransactionModel MapToModel(TransactionDataModelSQL dataModel)
    {
        return new TransactionModel
        {
            Id = dataModel.Id,
            BuyerId = dataModel.BuyerId,
            ListingId = dataModel.ListingId,
            AgreedPrice = dataModel.AgreedPrice,
            TransactionDate = dataModel.TransactionDate,
            Status = dataModel.Status
        };
    }

    public static TransactionDataModelSQL MapFromDomein(TransactionModel transactionModel)
    {
        return new TransactionDataModelSQL
        {
            Id = transactionModel.Id,
            BuyerId = transactionModel.BuyerId,
            ListingId = transactionModel.ListingId,
            AgreedPrice = transactionModel.AgreedPrice,
            TransactionDate = transactionModel.TransactionDate,
            Status = transactionModel.Status
        };
    }
}