namespace CipherScore.Shared.DTOs.Requests;

/// <summary>
/// Options for password generation
/// </summary>
public record PasswordGenerationOptions(
    int Length = 12,
    bool IncludeUppercase = true,
    bool IncludeLowercase = true,
    bool IncludeNumbers = true,
    bool IncludeSpecialChars = true,
    bool ExcludeSimilarChars = true,
    bool GeneratePassphrase = false,
    int PassphraseWordCount = 4,
    bool PassphraseIncludeNumbers = false,
    bool PassphraseCapitalizeWords = true,
    string PassphraseSeparator = "-",
    string[]? ExcludeWords = null);
