using Azure.Data.Tables;
using System.Security.Cryptography;
using System.Text;
using CipherScore.Shared.Models;
using CipherScore.Shared.DTOs.Responses;

namespace CipherScore.ApiService.Services;

public class PasswordStorageService
{
    private readonly TableClient _tableClient;

    public PasswordStorageService(TableClient tableClient)
    {
        _tableClient = tableClient;
        // Ensure table exists
        _tableClient.CreateIfNotExists();
    }

    /// <summary>
    /// Saves password analysis data to Azure Table Storage
    /// </summary>
    public async Task<PasswordEntry> SavePasswordEntryAsync(
        string password,
        PasswordAnalysisResult analysis,
        BreachCheckResult breachCheck,
        string? userGuess = null,
        CancellationToken cancellationToken = default)
    {
        var entry = new PasswordEntry
        {
            RowKey = Guid.NewGuid().ToString(),
            PartitionKey = "PasswordData",
            Password = HashPassword(password), // Store hashed password for privacy
            UserGuess = userGuess ?? string.Empty,
            StrengthRating = GetStrengthRating(analysis.Score),
            ActualBruteForceTime = EstimateBruteForceTime(password, analysis.Score),
            GuessAccuracy = CalculateGuessAccuracy(analysis.Score, userGuess),
            Timestamp = DateTimeOffset.UtcNow
        };

        await _tableClient.AddEntityAsync(entry, cancellationToken);
        return entry;
    }

    /// <summary>
    /// Retrieves recent password entries from Azure Table Storage
    /// </summary>
    public async Task<List<PasswordEntry>> GetRecentEntriesAsync(
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        var entries = new List<PasswordEntry>();
        
        await foreach (var entity in _tableClient.QueryAsync<PasswordEntry>(
            maxPerPage: limit,
            cancellationToken: cancellationToken))
        {
            entries.Add(entity);
            if (entries.Count >= limit) break;
        }

        return entries.OrderByDescending(e => e.Timestamp).Take(limit).ToList();
    }

    /// <summary>
    /// Gets statistics from stored password data
    /// </summary>
    public async Task<PasswordStorageStats> GetStorageStatsAsync(CancellationToken cancellationToken = default)
    {
        var entries = new List<PasswordEntry>();
        
        await foreach (var entity in _tableClient.QueryAsync<PasswordEntry>(cancellationToken: cancellationToken))
        {
            entries.Add(entity);
        }

        var strengthDistribution = entries
            .GroupBy(e => e.StrengthRating)
            .ToDictionary(g => g.Key, g => g.Count());

        return new PasswordStorageStats(
            TotalEntries: entries.Count,
            StrengthDistribution: strengthDistribution,
            LastUpdated: entries.Any() ? entries.Max(e => e.Timestamp ?? DateTimeOffset.MinValue) : DateTimeOffset.UtcNow
        );
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(hashedBytes);
    }

    private string GetStrengthRating(int score)
    {
        return score switch
        {
            >= 80 => "Strong",
            >= 60 => "Moderate",
            >= 40 => "Weak",
            _ => "Very Weak"
        };
    }

    private string EstimateBruteForceTime(string password, int score)
    {
        // Simple estimation based on character set and length
        var charSetSize = 0;
        if (password.Any(char.IsLower)) charSetSize += 26;
        if (password.Any(char.IsUpper)) charSetSize += 26;
        if (password.Any(char.IsDigit)) charSetSize += 10;
        if (password.Any(c => !char.IsLetterOrDigit(c))) charSetSize += 32;

        var combinations = Math.Pow(charSetSize, password.Length);
        var attemptsPerSecond = 1_000_000_000; // 1 billion attempts/sec (modern GPU)
        var seconds = combinations / attemptsPerSecond;

        return seconds switch
        {
            < 1 => "Instant",
            < 60 => $"{seconds:F0} seconds",
            < 3600 => $"{seconds / 60:F0} minutes",
            < 86400 => $"{seconds / 3600:F0} hours",
            < 31536000 => $"{seconds / 86400:F0} days",
            _ => $"{seconds / 31536000:F0} years"
        };
    }

    private string CalculateGuessAccuracy(int actualScore, string? userGuess)
    {
        if (string.IsNullOrEmpty(userGuess)) return "No guess provided";

        var guessScore = userGuess.ToLower() switch
        {
            "strong" or "very strong" => 80,
            "moderate" or "medium" => 60,
            "weak" => 40,
            "very weak" or "poor" => 20,
            _ => 50
        };

        var difference = Math.Abs(actualScore - guessScore);
        return difference switch
        {
            <= 10 => "Excellent",
            <= 20 => "Good",
            <= 30 => "Fair",
            _ => "Poor"
        };
    }
}

