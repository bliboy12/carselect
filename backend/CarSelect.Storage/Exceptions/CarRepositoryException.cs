public class CarRepositoryException : Exception
{
    public CarRepositoryException()
    {
    }

    public CarRepositoryException(string? message) : base(message)
    {
    }

    public CarRepositoryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}