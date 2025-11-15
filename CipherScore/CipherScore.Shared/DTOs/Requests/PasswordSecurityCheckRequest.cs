namespace CipherScore.Shared.DTOs.Requests;

/// <summary>
/// Request for comprehensive security check
/// </summary>
public record PasswordSecurityCheckRequest(
    string Password, 
    string? UserId = null, 
    bool SaveToHistory = false);
