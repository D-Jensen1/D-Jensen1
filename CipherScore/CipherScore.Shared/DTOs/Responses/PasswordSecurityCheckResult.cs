using CipherScore.Shared.Models;

namespace CipherScore.Shared.DTOs.Responses;

/// <summary>
/// Result of comprehensive password security check
/// </summary>
public record PasswordSecurityCheckResult(
    PasswordAnalysisResult StrengthAnalysis,
    BreachCheckResult BreachStatus,
    bool IsSecure,
    string[] SecurityRecommendations);

public record PasswordEvaluationResult(
     string AiSuggestions,       // Suggestions from the AI service
     long ElapsedMilliseconds    // Time taken for analysis in milliseconds
);
