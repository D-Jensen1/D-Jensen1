using CipherScore.Shared.Models;

namespace CipherScore.Shared.DTOs.Responses;

/// <summary>
/// Result of password submission to storage
/// </summary>
public record PasswordSubmissionResult(
    string Message,
    string EntryId,
    string StrengthRating,
    string BruteForceTime,
    string GuessAccuracy,
    bool IsBreached,
    PasswordAnalysisResult Analysis);
