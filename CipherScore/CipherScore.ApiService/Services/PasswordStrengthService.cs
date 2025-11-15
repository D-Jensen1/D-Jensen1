using System.Text.RegularExpressions;
using CipherScore.Shared.Models;

namespace CipherScore.ApiService.Services;

public class PasswordStrengthService
{
    public PasswordAnalysisResult AnalyzePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return new PasswordAnalysisResult();
        }

        var result = new PasswordAnalysisResult
        {
            Password = password,
            Strengths = new List<string>(),
            Suggestions = new List<string>(),
            Score = 0
        };

        AnalyzeLength(password, result);
        AnalyzeCharacterTypes(password, result);
        AnalyzeComplexity(password, result);
        AnalyzePatterns(password, result);

        // Cap the score at 100
        result.Score = Math.Min(result.Score, 100);

        return result;
    }

    private void AnalyzeLength(string password, PasswordAnalysisResult result)
    {
        if (password.Length >= 8)
        {
            result.Score += 20;
            result.Strengths.Add($"Good length ({password.Length} characters)");
        }
        else
        {
            result.Suggestions.Add($"Use at least 8 characters (currently {password.Length})");
        }

        if (password.Length >= 12)
        {
            result.Score += 10;
            result.Strengths.Add("Excellent length (12+ characters)");
        }
    }

    private void AnalyzeCharacterTypes(string password, PasswordAnalysisResult result)
    {
        bool hasLower = Regex.IsMatch(password, "[a-z]");
        bool hasUpper = Regex.IsMatch(password, "[A-Z]");
        bool hasDigit = Regex.IsMatch(password, "[0-9]");
        bool hasSpecial = Regex.IsMatch(password, "[^a-zA-Z0-9]");

        if (hasLower)
        {
            result.Score += 10;
            result.Strengths.Add("Contains lowercase letters");
        }
        else
        {
            result.Suggestions.Add("Add lowercase letters (a-z)");
        }

        if (hasUpper)
        {
            result.Score += 10;
            result.Strengths.Add("Contains uppercase letters");
        }
        else
        {
            result.Suggestions.Add("Add uppercase letters (A-Z)");
        }

        if (hasDigit)
        {
            result.Score += 10;
            result.Strengths.Add("Contains numbers");
        }
        else
        {
            result.Suggestions.Add("Add numbers (0-9)");
        }

        if (hasSpecial)
        {
            result.Score += 15;
            result.Strengths.Add("Contains special characters");
        }
        else
        {
            result.Suggestions.Add("Add special characters (!@#$%^&*)");
        }
    }

    private void AnalyzeComplexity(string password, PasswordAnalysisResult result)
    {
        int uniqueChars = password.Distinct().Count();
        if (uniqueChars >= password.Length * 0.7)
        {
            result.Score += 10;
            result.Strengths.Add("Good character variety");
        }
        else if (uniqueChars < password.Length * 0.5)
        {
            result.Suggestions.Add("Reduce repeated characters");
        }
    }

    private void AnalyzePatterns(string password, PasswordAnalysisResult result)
    {
        if (!HasCommonPatterns(password))
        {
            result.Score += 15;
            result.Strengths.Add("No common patterns detected");
        }
        else
        {
            result.Suggestions.Add("Avoid common patterns (123, abc, qwerty)");
        }
    }

    private bool HasCommonPatterns(string password)
    {
        string lower = password.ToLower();
        string[] commonPatterns = { "123", "abc", "qwe", "asd", "zxc", "password", "admin" };
        return commonPatterns.Any(pattern => lower.Contains(pattern));
    }

    public int GetUniqueCharCount(string password)
    {
        return string.IsNullOrEmpty(password) ? 0 : password.Distinct().Count();
    }

    public int GetCharacterTypes(string password)
    {
        if (string.IsNullOrEmpty(password)) return 0;

        int types = 0;
        if (Regex.IsMatch(password, "[a-z]")) types++;
        if (Regex.IsMatch(password, "[A-Z]")) types++;
        if (Regex.IsMatch(password, "[0-9]")) types++;
        if (Regex.IsMatch(password, "[^a-zA-Z0-9]")) types++;
        return types;
    }

    public double GetEntropyScore(string password)
    {
        if (string.IsNullOrEmpty(password)) return 0;
        
        var charSet = password.Distinct().Count();
        return Math.Round(password.Length * Math.Log2(charSet), 1);
    }

    public string GetStrengthText(int score)
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

    public string GetStrengthBadgeClass(int score)
    {
        return score switch
        {
            >= 80 => "bg-success",
            >= 60 => "bg-primary",
            >= 40 => "bg-warning",
            >= 20 => "bg-orange",
            _ => "bg-danger"
        };
    }

    public string GetStrengthProgressClass(int score)
    {
        return score switch
        {
            >= 80 => "bg-success",
            >= 60 => "bg-primary",
            >= 40 => "bg-warning",
            >= 20 => "bg-orange",
            _ => "bg-danger"
        };
    }
}