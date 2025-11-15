namespace CipherScore.Shared.DTOs.Responses;

/// <summary>
/// Generated password suggestion with metadata
/// </summary>
public record PasswordSuggestion(
    string Password,
    int EstimatedStrength,
    string[] UsedCriteria,
    string SecurityTip);
