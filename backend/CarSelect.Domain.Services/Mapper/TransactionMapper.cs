public class TransactionMapper
{
    public TransactionModel MapToDomein(TransactionDataModel transactionDataModel)
    {
        return new TransactionModel
        {
            Id = Guid.Parse(transactionDataModel.Id),
            BuyerId = Guid.Parse(transactionDataModel.BuyerId),
            ListingId = Guid.Parse(transactionDataModel.ListingId),
            AgreedPrice = transactionDataModel.AgreedPrice,
            PurchaseDate = transactionDataModel.TransactionDate,
            Status = transactionDataModel.Status.ToLower() == "successful" ? TransactionStatus.Successful : (transactionDataModel.Status.ToLower() == "pending" ? TransactionStatus.Pending : TransactionStatus.Rejected),
        };
    }

    public TransactionDataModel MapFromDomein(TransactionModel transactionModel)
    {
        return new TransactionDataModel
        {
            Id = transactionModel.Id.ToString(),
            BuyerId = transactionModel.BuyerId.ToString(),
            ListingId = transactionModel.ListingId.ToString(),
            AgreedPrice = transactionModel.AgreedPrice,
            TransactionDate = transactionModel.PurchaseDate,
            Status = transactionModel.Status.ToString()
        };
    }
}