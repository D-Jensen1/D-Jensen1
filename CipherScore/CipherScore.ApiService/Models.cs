using Azure;
using Azure.Data.Tables;

namespace CipherScore.ApiService
{
    public class PasswordEntry : ITableEntity
    {
        public string PartitionKey { get; set; } = "PasswordData";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string Password { get; set; } = string.Empty;
        public string UserGuess { get; set; } = string.Empty;
        public string StrengthRating { get; set; } = string.Empty;
        public string ActualBruteForceTime { get; set; } = string.Empty;
        public string GuessAccuracy { get; set; } = string.Empty;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
