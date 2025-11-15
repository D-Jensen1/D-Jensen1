using CipherScore.ApiService.Services;
using Moq;
using Xunit;

namespace CipherScore.ApiService.Tests.Services;

public class PasswordSecurityServiceTests
{
    private readonly Mock<HaveIBeenPwnedService> _mockHibpService;
    private readonly Mock<PasswordGeneratorService> _mockGeneratorService;
    private readonly PasswordSecurityService _service;

    public PasswordSecurityServiceTests()
    {
        var mockHttpClient = new Mock<HttpClient>();
        _mockHibpService = new Mock<HaveIBeenPwnedService>(mockHttpClient.Object);
        _mockGeneratorService = new Mock<PasswordGeneratorService>();
        _service = new PasswordSecurityService(_mockHibpService.Object, _mockGeneratorService.Object);
    }

    [Fact]
    public void GetCommonPasswords_ReturnsListOfPasswords()
    {
        // Act
        var result = _service.GetCommonPasswords(10);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Length <= 10);
        Assert.Contains("password", result);
        Assert.Contains("123456", result);
    }

    [Fact]
    public void HashPassword_ReturnsValidSHA256Hash()
    {
        // Arrange
        var password = "testpassword";

        // Act
        var hash = _service.HashPassword(password);

        // Assert
        Assert.NotNull(hash);
        Assert.Equal(64, hash.Length); // SHA256 produces 64 hex characters
        Assert.Matches("^[A-F0-9]+$", hash); // Should be uppercase hex
    }

    [Fact]
    public void HashPassword_SamePassword_ReturnsSameHash()
    {
        // Arrange
        var password = "testpassword";

        // Act
        var hash1 = _service.HashPassword(password);
        var hash2 = _service.HashPassword(password);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GeneratePasswordSuggestion_DefaultOptions_ReturnsValidPassword()
    {
        // Arrange
        var options = new PasswordGenerationOptions();

        // Act
        var result = _service.GeneratePasswordSuggestion(options);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(12, result.Password.Length);
        Assert.True(result.EstimatedStrength > 0);
        Assert.NotEmpty(result.UsedCriteria);
        Assert.NotNull(result.SecurityTip);
    }

    [Fact]
    public void GeneratePasswordSuggestion_CustomLength_ReturnsCorrectLength()
    {
        // Arrange
        var options = new PasswordGenerationOptions(Length: 20);

        // Act
        var result = _service.GeneratePasswordSuggestion(options);

        // Assert
        Assert.Equal(20, result.Password.Length);
    }

    [Fact]
    public void GeneratePasswordSuggestion_NoSpecialChars_DoesNotIncludeSpecialChars()
    {
        // Arrange
        var options = new PasswordGenerationOptions(
            Length: 12,
            IncludeSpecialChars: false
        );

        // Act
        var result = _service.GeneratePasswordSuggestion(options);

        // Assert
        Assert.DoesNotContain(result.UsedCriteria, c => c.Contains("Special"));
        Assert.All(result.Password, c => Assert.True(char.IsLetterOrDigit(c)));
    }

    [Fact]
    public void GeneratePasswordSuggestion_ExcludeSimilarChars_DoesNotIncludeSimilarChars()
    {
        // Arrange
        var options = new PasswordGenerationOptions(
            Length: 50, // Longer to increase probability of finding excluded chars if they exist
            ExcludeSimilarChars: true
        );

        // Act
        var result = _service.GeneratePasswordSuggestion(options);

        // Assert - should not contain i, l, 1, L, o, 0, O
        var similarChars = new[] { 'i', 'l', '1', 'L', 'o', '0', 'O' };
        foreach (var c in similarChars)
        {
            Assert.DoesNotContain(c, result.Password);
        }
    }

    [Theory]
    [InlineData(8, true)]
    [InlineData(12, false)]
    [InlineData(16, false)]
    public void GeneratePasswordSuggestion_GivesAppropriateSecurityTip(int length, bool expectLengthTip)
    {
        // Arrange
        var options = new PasswordGenerationOptions(Length: length);

        // Act
        var result = _service.GeneratePasswordSuggestion(options);

        // Assert
        if (expectLengthTip)
        {
            Assert.Contains("longer password", result.SecurityTip.ToLower());
        }
    }

    [Fact]
    public void CheckPasswordBreach_SimulatedBreach_ReturnsConsistentResults()
    {
        // Arrange
        var passwordHash = "ABC123";

        // Act
        var result1 = _service.CheckPasswordBreach(passwordHash);
        var result2 = _service.CheckPasswordBreach(passwordHash);

        // Assert - should be deterministic based on hash
        Assert.Equal(result1.IsBreached, result2.IsBreached);
        Assert.Equal(result1.BreachCount, result2.BreachCount);
    }
}
