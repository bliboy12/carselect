public class ListingRepositoryException : Exception
{
    public ListingRepositoryException()
    {
    }

    public ListingRepositoryException(string? message) : base(message)
    {
    }

    public ListingRepositoryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}