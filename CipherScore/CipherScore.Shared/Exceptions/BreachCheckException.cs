namespace CipherScore.Shared.Exceptions;

/// <summary>
/// Exception thrown when breach check fails
/// </summary>
public class BreachCheckException : Exception
{
    public BreachCheckException() : base()
    {
    }

    public BreachCheckException(string message) : base(message)
    {
    }

    public BreachCheckException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
