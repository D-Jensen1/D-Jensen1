namespace CipherScore.Shared.Configuration;

/// <summary>
/// Configuration options for Azure Table Storage
/// </summary>
public class AzureStorageOptions
{
    public const string SectionName = "AzureStorage";
    
    public string ConnectionString { get; set; } = string.Empty;
    public string TableName { get; set; } = "PasswordEntries";
}
