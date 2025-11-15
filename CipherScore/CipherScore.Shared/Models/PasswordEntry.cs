using Azure;
using Azure.Data.Tables;

namespace CipherScore.Shared.Models;

/// <summary>
/// Represents a password entry stored in Azure Table Storage
/// </summary>
public class PasswordEntry : ITableEntity
{
    public string PartitionKey { get; set; } = string.Empty;
    public string RowKey { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserGuess { get; set; } = string.Empty;
    public string StrengthRating { get; set; } = string.Empty;
    public string ActualBruteForceTime { get; set; } = string.Empty;
    public string GuessAccuracy { get; set; } = string.Empty;
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
