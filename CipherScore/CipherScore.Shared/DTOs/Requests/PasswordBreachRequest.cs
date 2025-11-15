namespace CipherScore.Shared.DTOs.Requests;

/// <summary>
/// Request to check if password has been breached
/// </summary>
public record PasswordBreachRequest(string Password);
