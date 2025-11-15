using CipherScore.Shared.Models;
using CipherScore.Shared.DTOs.Responses;

namespace CipherScore.ApiService.Services;

public class PasswordHistoryService
{
    private static readonly List<AnalysisHistoryEntry> _history = new();
    private static readonly object _lock = new();

    public AnalysisHistoryEntry SaveAnalysisHistory(string password, PasswordAnalysisResult analysis, string? userId = null)
    {
        var entry = new AnalysisHistoryEntry(
            Id: Guid.NewGuid().ToString(),
            Timestamp: DateTime.UtcNow,
            PasswordHash: HashPassword(password),
            StrengthScore: analysis.Score,
            StrengthLevel: GetStrengthLevel(analysis.Score),
            UserId: userId
        );

        lock (_lock)
        {
            _history.Add(entry);
            
            // Keep only last 1000 entries to prevent memory issues
            if (_history.Count > 1000)
            {
                _history.RemoveRange(0, _history.Count - 1000);
            }
        }

        return entry;
    }

    public IEnumerable<AnalysisHistoryEntry> GetAnalysisHistory(int limit = 50)
    {
        lock (_lock)
        {
            return _history
                .OrderByDescending(h => h.Timestamp)
                .Take(limit)
                .ToList();
        }
    }

    public PasswordStatsResult GetPasswordStats()
    {
        lock (_lock)
        {
            if (!_history.Any())
            {
                return new PasswordStatsResult(
                    0, 0, new Dictionary<string, int>(), Array.Empty<string>(), DateTime.UtcNow);
            }

            var totalAnalyses = _history.Count;
            var averageScore = _history.Average(h => h.StrengthScore);
            
            var strengthDistribution = _history
                .GroupBy(h => h.StrengthLevel)
                .ToDictionary(g => g.Key, g => g.Count());

            // Simulate common weaknesses analysis
            var commonWeaknesses = new[]
            {
                "Too short (less than 8 characters)",
                "Missing uppercase letters",
                "Missing special characters",
                "Contains common patterns"
            };

            return new PasswordStatsResult(
                totalAnalyses,
                Math.Round(averageScore, 2),
                strengthDistribution,
                commonWeaknesses,
                DateTime.UtcNow
            );
        }
    }

    private string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(hashedBytes);
    }

    private string GetStrengthLevel(int score)
    {
        return score switch
        {
            >= 80 => "Very Strong",
            >= 60 => "Strong",
            >= 40 => "Moderate",
            >= 20 => "Weak",
            _ => "Very Weak"
        };
    }
}