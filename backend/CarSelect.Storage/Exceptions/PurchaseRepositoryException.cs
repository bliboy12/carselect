public class PurchaseRepositoryException : Exception
{
    public PurchaseRepositoryException()
    {
    }

    public PurchaseRepositoryException(string? message) : base(message)
    {
    }

    public PurchaseRepositoryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}