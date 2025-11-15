namespace CipherScore.Shared.DTOs.Requests;

/// <summary>
/// Request to submit password for analysis and storage
/// </summary>
public record PasswordSubmissionRequest(string Password, string UserGuess, string UserId);
