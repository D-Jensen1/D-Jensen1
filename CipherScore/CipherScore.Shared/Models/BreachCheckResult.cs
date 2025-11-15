namespace CipherScore.Shared.Models;

/// <summary>
/// Represents the result of a breach check using HaveIBeenPwned or similar service
/// </summary>
public record BreachCheckResult(
    bool IsBreached, 
    int BreachCount, 
    string? LastBreachDate = null, 
    string? Source = null);
