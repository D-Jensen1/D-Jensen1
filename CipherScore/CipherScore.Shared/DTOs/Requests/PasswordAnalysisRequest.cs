namespace CipherScore.Shared.DTOs.Requests;

/// <summary>
/// Request to analyze password strength
/// </summary>
public record PasswordAnalysisRequest(string Password, string? UserId = null);
