using CipherScore.Shared.DTOs.Requests;
using CipherScore.Shared.DTOs.Responses;
using CipherScore.Shared.Models;
using System.Net.Http;
using System.Threading;

namespace CipherScore.Web;

public class CipherApiClient(HttpClient httpClient)
{
    /// <summary>
    /// Analyzes password strength using the server-side API
    /// </summary>
    public async Task<PasswordAnalysisResult> AnalyzePasswordAsync(string password, CancellationToken cancellationToken = default)
    {
        var request = new PasswordAnalysisRequest(password);
        var response = await httpClient.PostAsJsonAsync("/api/password/analyze", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<PasswordAnalysisResult>(cancellationToken) 
               ?? new PasswordAnalysisResult();
    }
    
    //temporary until we use shareproject 
    public class PasswordRequest
    {
        public string Password { get; set; }
    }
    public async Task<PasswordEvaluationResult> GetAiSuggestionsAsync(string password)
    {
        var request = new PasswordRequest { Password = password };
        var response = await httpClient.PostAsJsonAsync("api/password/ai-suggestions", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PasswordEvaluationResult>()
       ?? new PasswordEvaluationResult(
           String.Empty,
           0  // ElapsedMilliseconds
          );

        //return await response.Content.ReadFromJsonAsync<string[]>();
    }


    /// <summary>
    /// Checks if a password has been compromised using HaveIBeenPwned API
    /// </summary>
    public async Task<BreachCheckResult> CheckPasswordBreachAsync(string password, CancellationToken cancellationToken = default)
    {
        var request = new PasswordBreachRequest(password);
        var response = await httpClient.PostAsJsonAsync("/api/password/breach-check", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<BreachCheckResult>(cancellationToken) 
               ?? new BreachCheckResult(false, 0);
    }

    /// <summary>
    /// Performs comprehensive security check including strength analysis and breach checking
    /// </summary>
    public async Task<PasswordSecurityCheckResult> CheckPasswordSecurityAsync(string password, string? userId = null, bool saveToHistory = false, CancellationToken cancellationToken = default)
    {
        var request = new PasswordSecurityCheckRequest(password, userId, saveToHistory);
        var response = await httpClient.PostAsJsonAsync("/api/password/security-check", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<PasswordSecurityCheckResult>(cancellationToken) 
               ?? new PasswordSecurityCheckResult(
                   new PasswordAnalysisResult(), 
                   new BreachCheckResult(false, 0), 
                   false, 
                   Array.Empty<string>());
    }

    /// <summary>
    /// Legacy method - Checks if a password hash has been found in known data breaches
    /// </summary>
    public async Task<BreachCheckResult> CheckPasswordBreachByHashAsync(string passwordHash, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/api/password/breach-check/{passwordHash}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<BreachCheckResult>(cancellationToken) 
               ?? new BreachCheckResult(false, 0);
    }

    /// <summary>
    /// Gets a list of common passwords to avoid
    /// </summary>
    public async Task<string[]> GetCommonPasswordsAsync(int maxItems = 100, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/api/password/common-passwords?limit={maxItems}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<string[]>(cancellationToken) ?? [];
    }

    /// <summary>
    /// Saves password analysis history (password is hashed for privacy)
    /// </summary>
    public async Task<AnalysisHistoryEntry> SaveAnalysisHistoryAsync(PasswordAnalysisRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/password/history", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<AnalysisHistoryEntry>(cancellationToken) 
               ?? new AnalysisHistoryEntry("", DateTime.UtcNow, "", 0, "");
    }

    /// <summary>
    /// Gets password analysis history for analytics
    /// </summary>
    public async Task<AnalysisHistoryEntry[]> GetAnalysisHistoryAsync(int maxItems = 50, CancellationToken cancellationToken = default)
    {
        List<AnalysisHistoryEntry>? history = null;

        await foreach (var entry in httpClient.GetFromJsonAsAsyncEnumerable<AnalysisHistoryEntry>($"/api/password/history?limit={maxItems}", cancellationToken))
        {
            if (history?.Count >= maxItems)
            {
                break;
            }
            if (entry is not null)
            {
                history ??= [];
                history.Add(entry);
            }
        }

        return history?.ToArray() ?? [];
    }

    /// <summary>
    /// Gets password strength statistics and trends
    /// </summary>
    public async Task<PasswordStatsResult> GetPasswordStatsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/api/password/stats", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<PasswordStatsResult>(cancellationToken) 
               ?? new PasswordStatsResult(0, 0, new Dictionary<string, int>(), [], DateTime.UtcNow);
    }

    /// <summary>
    /// Generates a secure password suggestion
    /// </summary>
    public async Task<PasswordSuggestion> GeneratePasswordSuggestionAsync(PasswordGenerationOptions options, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/password/generate", options, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<PasswordSuggestion>(cancellationToken) 
               ?? new PasswordSuggestion("", 0, [], "");
    }

    // ========== NEW METHODS FOR AZURE TABLE STORAGE ==========

    /// <summary>
    /// Submits a password to Azure Table Storage with analysis
    /// </summary>
    public async Task<PasswordSubmissionResult> SubmitPasswordAsync(string password, string? userGuess = null, string? userId = null, CancellationToken cancellationToken = default)
    {
        var request = new PasswordSubmissionRequest(password, userGuess ?? "", userId ?? "anonymous");
        var response = await httpClient.PostAsJsonAsync("/api/password/submit", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<PasswordSubmissionResult>(cancellationToken) 
               ?? new PasswordSubmissionResult("", "", "", "", "", false, new PasswordAnalysisResult());
    }

    /// <summary>
    /// Gets recent password submissions from Azure Table Storage
    /// </summary>
    public async Task<PasswordEntry[]> GetPasswordSubmissionsAsync(int limit = 50, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/api/password/submissions?limit={limit}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<PasswordEntry[]>(cancellationToken) ?? [];
    }

    /// <summary>
    /// Gets statistics from Azure Table Storage
    /// </summary>
    public async Task<PasswordStorageStats> GetStorageStatsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/api/password/storage-stats", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<PasswordStorageStats>(cancellationToken) 
               ?? new PasswordStorageStats(0, new Dictionary<string, int>(), DateTimeOffset.UtcNow);
    }
}
