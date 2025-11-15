namespace CipherScore.Shared.Exceptions;

/// <summary>
/// Exception thrown when password analysis fails
/// </summary>
public class PasswordAnalysisException : Exception
{
    public PasswordAnalysisException() : base()
    {
    }

    public PasswordAnalysisException(string message) : base(message)
    {
    }

    public PasswordAnalysisException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
