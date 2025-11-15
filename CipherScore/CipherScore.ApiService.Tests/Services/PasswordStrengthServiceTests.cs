using CipherScore.ApiService.Services;
using CipherScore.Shared.Models;
using Xunit;

namespace CipherScore.ApiService.Tests.Services;

public class PasswordStrengthServiceTests
{
    private readonly PasswordStrengthService _service;

    public PasswordStrengthServiceTests()
    {
        _service = new PasswordStrengthService();
    }

    [Fact]
    public void AnalyzePassword_EmptyPassword_ReturnsZeroScore()
    {
        // Arrange
        var password = "";

        // Act
        var result = _service.AnalyzePassword(password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.Score);
    }

    [Fact]
    public void AnalyzePassword_ShortPassword_ReturnsSuggestion()
    {
        // Arrange
        var password = "abc";

        // Act
        var result = _service.AnalyzePassword(password);

        // Assert
        Assert.Contains(result.Suggestions, s => s.Contains("at least 8 characters"));
    }

    [Fact]
    public void AnalyzePassword_StrongPassword_ReturnsHighScore()
    {
        // Arrange
        var password = "MyP@ssw0rd123!";

        // Act
        var result = _service.AnalyzePassword(password);

        // Assert
        Assert.True(result.Score >= 60, $"Expected score >= 60, but got {result.Score}");
        Assert.NotEmpty(result.Strengths);
    }

    [Theory]
    [InlineData("password", true)]
    [InlineData("P@ssw0rd!23", false)]
    [InlineData("abcdefgh", true)]
    public void AnalyzePassword_ChecksCommonPatterns(string password, bool shouldHavePattern)
    {
        // Act
        var result = _service.AnalyzePassword(password);

        // Assert
        var hasPatternSuggestion = result.Suggestions.Any(s => s.Contains("common patterns"));
        
        if (shouldHavePattern)
        {
            Assert.True(hasPatternSuggestion, "Expected to find common pattern warning");
        }
        else
        {
            Assert.False(hasPatternSuggestion, "Expected no common pattern warning");
        }
    }

    [Fact]
    public void AnalyzePassword_AllCharacterTypes_ReturnsAllStrengths()
    {
        // Arrange
        var password = "Abc123!@#";

        // Act
        var result = _service.AnalyzePassword(password);

        // Assert
        Assert.Contains(result.Strengths, s => s.Contains("lowercase"));
        Assert.Contains(result.Strengths, s => s.Contains("uppercase"));
        Assert.Contains(result.Strengths, s => s.Contains("numbers"));
        Assert.Contains(result.Strengths, s => s.Contains("special"));
    }

    [Theory]
    [InlineData(95, "Very Strong")]
    [InlineData(75, "Strong")]
    [InlineData(50, "Moderate")]
    [InlineData(30, "Weak")]
    [InlineData(10, "Very Weak")]
    public void GetStrengthText_ReturnsCorrectLabel(int score, string expectedLabel)
    {
        // Act
        var result = _service.GetStrengthText(score);

        // Assert
        Assert.Equal(expectedLabel, result);
    }

    [Fact]
    public void AnalyzePassword_ScoreNeverExceeds100()
    {
        // Arrange - very strong password
        var password = "SuperStr0ng!P@ssw0rd#With$ManyChar@cters123";

        // Act
        var result = _service.AnalyzePassword(password);

        // Assert
        Assert.True(result.Score <= 100, $"Score should not exceed 100, but got {result.Score}");
    }

    [Fact]
    public void AnalyzePassword_NullPassword_ReturnsEmptyResult()
    {
        // Arrange
        string? password = null;

        // Act
        var result = _service.AnalyzePassword(password!);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.Score);
    }
}
