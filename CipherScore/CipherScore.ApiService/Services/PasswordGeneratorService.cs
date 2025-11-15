using CipherScore.Shared.DTOs.Requests;
using CipherScore.Shared.DTOs.Responses;
using MurrayGrant.ReadablePassphrase;
using MurrayGrant.ReadablePassphrase.Mutators;
using RndF;
using System.Security.Cryptography;
using System.Text;
using CipherScore.Shared.DTOs.Requests;
using CipherScore.Shared.DTOs.Responses;



namespace CipherScore.ApiService.Services;

/// <summary>
/// Service for generating secure passwords with customizable options
/// </summary>
public class PasswordGeneratorService
{
    /// <summary>
    /// Generates a secure password based on the provided options
    /// </summary>
    public PasswordSuggestion GeneratePassword(PasswordGenerationOptions options)
    {
        var chars = BuildCharacterSet(options);

        if (string.IsNullOrEmpty(chars) && !options.GeneratePassphrase)
        {
            throw new ArgumentException("At least one character type must be selected");
        }

        var password = GenerateSecurePassword(chars, options);
        var strength = EstimatePasswordStrength(password);
        var criteria = GetUsedCriteria(options);
        var tip = GetSecurityTip(options);

        return new PasswordSuggestion(password, strength, criteria, tip);
    }

    /// <summary>
    /// Builds a character set based on the selected options
    /// </summary>
    private string BuildCharacterSet(PasswordGenerationOptions options)
    {
        var chars = new StringBuilder();

        if (options.IncludeLowercase)
            chars.Append("abcdefghijklmnopqrstuvwxyz");

        if (options.IncludeUppercase)
            chars.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        if (options.IncludeNumbers)
            chars.Append("0123456789");

        if (options.IncludeSpecialChars)
            chars.Append("!@#$%^&*()_+-=[]{}|;:,.<>?");

        var result = chars.ToString();

        // Exclude similar characters if requested
        if (options.ExcludeSimilarChars && !string.IsNullOrEmpty(result))
        {
            var similar = new[] { 'i', 'l', '1', 'L', 'o', '0', 'O' };
            result = new string(result.Where(c => !similar.Contains(c)).ToArray());
        }

        return result;
    }

    /// <summary>
    /// Generates a cryptographically secure random password
    /// </summary>
    private string GenerateSecurePassword(string characterSet, PasswordGenerationOptions options)
    {
        using var rng = RandomNumberGenerator.Create();
        var password = new StringBuilder(options.Length);
        var bytes = new byte[4];

        if (options.GeneratePassphrase)
        {
            // Generate a passphrase using RndF library
            // Handle special separators: Mixed ("R") and No-separator (empty string)
            // Always generate words using space as a temporary separator
            var basePass = Rnd.StringF.Passphrase(
                numberOfWords: options.PassphraseWordCount,
                separator: ' ',
                upperFirst: options.PassphraseCapitalizeWords,
                includeNumber: options.PassphraseIncludeNumbers
            );

            var words = basePass.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Mixed separators: choose a random separator between each word
            if (!string.IsNullOrEmpty(options.PassphraseSeparator) && options.PassphraseSeparator[0] == 'R')
            {
                var separators = new[] { '-', '_', '.', '!', '@', '#', '$' };
                var sb = new StringBuilder();

                for (int i = 0; i < words.Length; i++)
                {
                    sb.Append(words[i]);
                    if (i < words.Length - 1)
                    {
                        int idx = RandomNumberGenerator.GetInt32(0, separators.Length);
                        sb.Append(separators[idx]);
                    }
                }

                return sb.ToString();
            }

            // No separator requested
            if (string.IsNullOrEmpty(options.PassphraseSeparator))
            {
                return string.Concat(words);
            }

            // Single-character separator
            char separatorChar = options.PassphraseSeparator[0];
            return string.Join(separatorChar, words);
        }
        

        var result = password.ToString();

        // Ensure we don't include excluded words
        if (options.ExcludeWords != null && options.ExcludeWords.Length > 0)
        {
            foreach (var word in options.ExcludeWords)
            {
                if (!string.IsNullOrEmpty(word) &&
                       result.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    // Regenerate if excluded word found (with recursion limit to prevent infinite loops)
                    return GenerateSecurePassword(characterSet, options);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Estimates password strength based on length and character variety
    /// </summary>
    private int EstimatePasswordStrength(string password)
    {
        if (string.IsNullOrEmpty(password))
            return 0;

        // Length component (up to 50 points)
        var score = Math.Min(password.Length * 4, 50);

        // Character type diversity
        var hasLower = password.Any(char.IsLower);
        var hasUpper = password.Any(char.IsUpper);
        var hasDigit = password.Any(char.IsDigit);
        var hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));

        if (hasLower) score += 10;
        if (hasUpper) score += 10;
        if (hasDigit) score += 10;
        if (hasSpecial) score += 15;

        // Bonus for high character diversity
        var uniqueChars = password.Distinct().Count();
        if (uniqueChars >= password.Length * 0.8)
            score += 5;

        return Math.Min(score, 100);
    }

    /// <summary>
    /// Gets a list of criteria used in password generation
    /// </summary>
    private string[] GetUsedCriteria(PasswordGenerationOptions options)
    {
        var criteria = new List<string>();

        if (options.GeneratePassphrase)
        {
            criteria.Add($"{options.PassphraseWordCount} words");
            if (options.PassphraseCapitalizeWords) criteria.Add("Capitalized words");
            if (options.PassphraseIncludeNumbers) criteria.Add("Includes numbers");
            if (!string.IsNullOrEmpty(options.PassphraseSeparator))
                criteria.Add($"Separator: '{options.PassphraseSeparator}'");
            else
                criteria.Add("No separator");
        }
        else
        {
            if (options.IncludeLowercase) criteria.Add("Lowercase letters");
            if (options.IncludeUppercase) criteria.Add("Uppercase letters");
            if (options.IncludeNumbers) criteria.Add("Numbers");
            if (options.IncludeSpecialChars) criteria.Add("Special characters");
            if (options.ExcludeSimilarChars) criteria.Add("No similar characters");
   
            criteria.Add($"{options.Length} characters long");
        }

        return criteria.ToArray();
    }

    /// <summary>
    /// Provides security tips based on the generation options
    /// </summary>
    private string GetSecurityTip(PasswordGenerationOptions options)
    {
        if (options.GeneratePassphrase)
        {
            if (options.PassphraseWordCount < 4)
                return "Consider using at least 4 words for better security.";
            
            if (!options.PassphraseIncludeNumbers && !options.PassphraseCapitalizeWords)
                return "Adding numbers or capitalization increases passphrase strength.";
            
            if (options.PassphraseWordCount >= 6)
                return "Excellent! This passphrase length provides strong security while being memorable.";
            
            return "Passphrases are easier to remember and type than random passwords!";
        }

        if (options.Length < 12)
            return "Consider using a longer password (12+ characters) for better security.";

        if (!options.IncludeSpecialChars)
            return "Adding special characters increases password strength significantly.";

        if (!options.IncludeUppercase || !options.IncludeLowercase)
            return "Mix uppercase and lowercase letters for stronger passwords.";

        if (options.Length >= 16)
            return "Excellent! This password length provides strong security.";

        return "This password meets strong security criteria. Store it securely!";
    }

    /// <summary>
    /// Validates password generation options
    /// </summary>
    public bool ValidateOptions(PasswordGenerationOptions options, out string errorMessage)
    {
        if (options.Length < 4)
        {
            errorMessage = "Password length must be at least 4 characters";
            return false;
        }

        if (options.Length > 128)
        {
            errorMessage = "Password length cannot exceed 128 characters";
            return false;
        }

        if (!options.IncludeUppercase && !options.IncludeLowercase &&
 !options.IncludeNumbers && !options.IncludeSpecialChars)
        {
            errorMessage = "At least one character type must be selected";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }
}
