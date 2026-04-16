public class ReviewRepositoryException : Exception
{
    public ReviewRepositoryException()
    {
    }

    public ReviewRepositoryException(string? message) : base(message)
    {
    }

    public ReviewRepositoryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}