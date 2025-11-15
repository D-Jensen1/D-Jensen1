namespace CipherScore.Shared.Exceptions;

/// <summary>
/// Exception thrown when Azure Storage operations fail
/// </summary>
public class AzureStorageException : Exception
{
    public AzureStorageException() : base()
    {
    }

    public AzureStorageException(string message) : base(message)
    {
    }

    public AzureStorageException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
