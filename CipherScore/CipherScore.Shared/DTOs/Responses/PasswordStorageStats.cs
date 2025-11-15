namespace CipherScore.Shared.DTOs.Responses;

/// <summary>
/// Statistics from Azure Table Storage
/// </summary>
public record PasswordStorageStats(
    int TotalEntries,
    Dictionary<string, int> StrengthDistribution,
    DateTimeOffset LastUpdated);
