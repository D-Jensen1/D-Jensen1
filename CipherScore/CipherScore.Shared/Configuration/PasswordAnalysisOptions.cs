namespace CipherScore.Shared.Configuration;

/// <summary>
/// Configuration options for password analysis
/// </summary>
public class PasswordAnalysisOptions
{
    public const string SectionName = "PasswordAnalysis";
    
    public int MinimumLength { get; set; } = 8;
    public int RecommendedLength { get; set; } = 12;
    public bool RequireUppercase { get; set; } = true;
    public bool RequireLowercase { get; set; } = true;
    public bool RequireDigits { get; set; } = true;
    public bool RequireSpecialChars { get; set; } = true;
    public int MinimumScore { get; set; } = 60;
}
