namespace CipherScore.Shared.Models;

/// <summary>
/// Represents the result of a password strength analysis
/// </summary>
public class PasswordAnalysisResult
{
    public string Password { get; set; } = string.Empty;
    public int Score { get; set; }
    public List<string> Strengths { get; set; } = new();
    public List<string> Suggestions { get; set; } = new();
}
