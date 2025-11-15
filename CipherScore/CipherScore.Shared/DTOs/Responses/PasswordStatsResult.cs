namespace CipherScore.Shared.DTOs.Responses;

/// <summary>
/// Password strength statistics and trends
/// </summary>
public record PasswordStatsResult(
    int TotalAnalyses,
    double AverageScore,
    Dictionary<string, int> StrengthDistribution,
    string[] MostCommonWeaknesses,
    DateTime LastUpdated);
