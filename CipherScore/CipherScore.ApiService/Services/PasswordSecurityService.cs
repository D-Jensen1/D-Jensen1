using System.Security.Cryptography;
using System.Text;
using CipherScore.Shared.Models;
using CipherScore.Shared.DTOs.Requests;
using CipherScore.Shared.DTOs.Responses;

namespace CipherScore.ApiService.Services;

public class PasswordSecurityService
{
    private readonly HaveIBeenPwnedService _haveIBeenPwnedService;
    private readonly PasswordGeneratorService _passwordGeneratorService;
    
    private static readonly string[] CommonPasswords = {
        "123456", "password", "123456789", "12345678", "12345", "1234567", "1234567890",
        "qwerty", "abc123", "111111", "password1", "1234", "123123", "welcome", "admin",
        "letmein", "monkey", "dragon", "master", "123", "login", "pass", "mustang", "shadow"
    };

    public PasswordSecurityService(
      HaveIBeenPwnedService haveIBeenPwnedService,
        PasswordGeneratorService passwordGeneratorService)
    {
        _haveIBeenPwnedService = haveIBeenPwnedService;
        _passwordGeneratorService = passwordGeneratorService;
    }

    public async Task<BreachCheckResult> CheckPasswordBreachAsync(string password, CancellationToken cancellationToken = default)
    {
        // Use HaveIBeenPwned service for real breach checking
 return await _haveIBeenPwnedService.CheckPasswordAsync(password, cancellationToken);
    }

    public BreachCheckResult CheckPasswordBreach(string passwordHash)
    {
   // Legacy method - simulate breach checking for backwards compatibility
var random = new Random(passwordHash.GetHashCode());
        var isBreached = random.Next(0, 100) < 15; // 15% chance of being breached for demo
  var breachCount = isBreached ? random.Next(1, 50000) : 0;
        
        return new BreachCheckResult(
     isBreached, 
          breachCount, 
            isBreached ? DateTime.UtcNow.AddDays(-random.Next(1, 365)).ToString("yyyy-MM-dd") : null,
            "Simulated"
    );
    }

    public string[] GetCommonPasswords(int limit = 100)
    {
        return CommonPasswords.Take(Math.Min(limit, CommonPasswords.Length)).ToArray();
    }

    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
     return Convert.ToHexString(hashedBytes);
    }

    public PasswordSuggestion GeneratePasswordSuggestion(PasswordGenerationOptions options)
    {
   // Delegate to PasswordGeneratorService
     return _passwordGeneratorService.GeneratePassword(options);
    }
}