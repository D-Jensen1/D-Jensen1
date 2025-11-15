using System.Security.Cryptography;
using System.Text;
using CipherScore.Shared.Models;

namespace CipherScore.ApiService.Services;

public class HaveIBeenPwnedService
{
    private readonly HttpClient _httpClient;
    private const string HIBP_API_BASE = "https://api.pwnedpasswords.com";

    public HaveIBeenPwnedService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Set User-Agent as required by HaveIBeenPwned API
        _httpClient.DefaultRequestHeaders.UserAgent.Clear();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("CipherScore/1.0");
    }

    /// <summary>
    /// Checks if a password has been compromised using the HaveIBeenPwned API
    /// Uses k-anonymity model - only sends first 5 characters of SHA-1 hash
    /// </summary>
    public async Task<BreachCheckResult> CheckPasswordAsync(string password, CancellationToken cancellationToken = default)
    {
        try
        {
            // Generate SHA-1 hash of the password
            var sha1Hash = ComputeSha1Hash(password);
            
            // Use k-anonymity: send only first 5 characters of hash
            var hashPrefix = sha1Hash[..5];
            var hashSuffix = sha1Hash[5..];

            // Query HaveIBeenPwned API
            var response = await _httpClient.GetAsync($"{HIBP_API_BASE}/range/{hashPrefix}", cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                // If API is unavailable, return unknown status
                return new BreachCheckResult(false, 0, null, "API unavailable");
            }

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            
            // Parse response to find if our hash suffix exists
            var breachCount = ParseHibpResponse(responseContent, hashSuffix);
            
            return new BreachCheckResult(
                IsBreached: breachCount > 0,
                BreachCount: breachCount,
                LastBreachDate: breachCount > 0 ? "Multiple breaches" : null,
                Source: "HaveIBeenPwned"
            );
        }
        catch (Exception ex)
        {
            // Log error in production, for now return safe default
            return new BreachCheckResult(false, 0, null, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Computes SHA-1 hash of the password (required by HaveIBeenPwned API)
    /// </summary>
    private static string ComputeSha1Hash(string password)
    {
        using var sha1 = SHA1.Create();
        var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(hashBytes);
    }

    /// <summary>
    /// Parses HaveIBeenPwned API response to find breach count for specific hash
    /// </summary>
    private static int ParseHibpResponse(string response, string hashSuffix)
    {
        var lines = response.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var line in lines)
        {
            var parts = line.Split(':');
            if (parts.Length == 2 && 
                string.Equals(parts[0].Trim(), hashSuffix, StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(parts[1].Trim(), out var count))
                {
                    return count;
                }
            }
        }
        
        return 0; // Not found in breaches
    }

    /// <summary>
    /// Checks multiple passwords at once (for batch operations)
    /// </summary>
    public async Task<Dictionary<string, BreachCheckResult>> CheckMultiplePasswordsAsync(
        IEnumerable<string> passwords, 
        CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, BreachCheckResult>();
        
        // Group passwords by hash prefix to minimize API calls
        var hashGroups = passwords
            .Distinct()
            .GroupBy(p => ComputeSha1Hash(p)[..5])
            .ToList();

        foreach (var group in hashGroups)
        {
            try
            {
                var hashPrefix = group.Key;
                var response = await _httpClient.GetAsync($"{HIBP_API_BASE}/range/{hashPrefix}", cancellationToken);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    
                    foreach (var password in group)
                    {
                        var fullHash = ComputeSha1Hash(password);
                        var hashSuffix = fullHash[5..];
                        var breachCount = ParseHibpResponse(responseContent, hashSuffix);
                        
                        results[password] = new BreachCheckResult(
                            IsBreached: breachCount > 0,
                            BreachCount: breachCount,
                            LastBreachDate: breachCount > 0 ? "Multiple breaches" : null,
                            Source: "HaveIBeenPwned"
                        );
                    }
                }
                else
                {
                    // If API call fails, mark as unknown for this group
                    foreach (var password in group)
                    {
                        results[password] = new BreachCheckResult(false, 0, null, "API unavailable");
                    }
                }

                // Rate limiting: small delay between requests
                await Task.Delay(100, cancellationToken);
            }
            catch (Exception ex)
            {
                // Handle individual group failures
                foreach (var password in group)
                {
                    results[password] = new BreachCheckResult(false, 0, null, $"Error: {ex.Message}");
                }
            }
        }

        return results;
    }
}