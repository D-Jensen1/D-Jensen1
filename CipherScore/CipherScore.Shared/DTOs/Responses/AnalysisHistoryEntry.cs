namespace CipherScore.Shared.DTOs.Responses;

/// <summary>
/// Analysis history entry for tracking
/// </summary>
public record AnalysisHistoryEntry(
    string Id,
    DateTime Timestamp,
    string PasswordHash,
    int StrengthScore,
    string StrengthLevel,
    string? UserId = null);
