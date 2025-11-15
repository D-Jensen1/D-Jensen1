namespace CipherScore.Shared.Configuration;

/// <summary>
/// Configuration options for HaveIBeenPwned API integration
/// </summary>
public class HaveIBeenPwnedOptions
{
    public const string SectionName = "HaveIBeenPwned";
    
    public string ApiBaseUrl { get; set; } = "https://api.pwnedpasswords.com";
    public int TimeoutSeconds { get; set; } = 10;
    public string UserAgent { get; set; } = "CipherScore/1.0";
}
