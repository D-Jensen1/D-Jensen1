using CipherScore.ApiService.Services;
using Xunit;

namespace CipherScore.ApiService.Tests.Services;

public class PasswordGeneratorServiceTests
{
    private readonly PasswordGeneratorService _service;

    public PasswordGeneratorServiceTests()
    {
   _service = new PasswordGeneratorService();
    }

    [Fact]
    public void GeneratePassword_WithDefaultOptions_ReturnsValidPassword()
    {
        // Arrange
        var options = new PasswordGenerationOptions();

        // Act
        var result = _service.GeneratePassword(options);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Password);
     Assert.Equal(12, result.Password.Length); // Default length
        Assert.True(result.EstimatedStrength > 0);
        Assert.NotEmpty(result.UsedCriteria);
    }

    [Theory]
    [InlineData(8)]
    [InlineData(16)]
    [InlineData(24)]
    [InlineData(32)]
    public void GeneratePassword_WithCustomLength_ReturnsCorrectLength(int length)
    {
      // Arrange
     var options = new PasswordGenerationOptions(Length: length);

// Act
        var result = _service.GeneratePassword(options);

 // Assert
        Assert.Equal(length, result.Password.Length);
    }

    [Fact]
    public void GeneratePassword_OnlyLowercase_ContainsOnlyLowercaseLetters()
    {
     // Arrange
   var options = new PasswordGenerationOptions(
            Length: 20,
        IncludeUppercase: false,
 IncludeLowercase: true,
            IncludeNumbers: false,
  IncludeSpecialChars: false
     );

     // Act
        var result = _service.GeneratePassword(options);

        // Assert
      Assert.All(result.Password, c => Assert.True(char.IsLower(c)));
    }

  [Fact]
    public void GeneratePassword_OnlyUppercase_ContainsOnlyUppercaseLetters()
    {
    // Arrange
        var options = new PasswordGenerationOptions(
      Length: 20,
  IncludeUppercase: true,
            IncludeLowercase: false,
         IncludeNumbers: false,
            IncludeSpecialChars: false
    );

        // Act
      var result = _service.GeneratePassword(options);

   // Assert
  Assert.All(result.Password, c => Assert.True(char.IsUpper(c)));
    }

 [Fact]
    public void GeneratePassword_OnlyNumbers_ContainsOnlyDigits()
    {
        // Arrange
 var options = new PasswordGenerationOptions(
     Length: 20,
     IncludeUppercase: false,
            IncludeLowercase: false,
 IncludeNumbers: true,
 IncludeSpecialChars: false
        );

      // Act
        var result = _service.GeneratePassword(options);

        // Assert
        Assert.All(result.Password, c => Assert.True(char.IsDigit(c)));
    }

    [Fact]
    public void GeneratePassword_WithAllCharacterTypes_ContainsMixedCharacters()
    {
        // Arrange
  var options = new PasswordGenerationOptions(
    Length: 50, // Longer to increase probability
     IncludeUppercase: true,
            IncludeLowercase: true,
         IncludeNumbers: true,
     IncludeSpecialChars: true
        );

        // Act
        var result = _service.GeneratePassword(options);

    // Assert - with high probability, a 50-char password should contain all types
        Assert.Contains(result.Password, c => char.IsLower(c));
   Assert.Contains(result.Password, c => char.IsUpper(c));
   Assert.Contains(result.Password, c => char.IsDigit(c));
        Assert.Contains(result.Password, c => !char.IsLetterOrDigit(c));
    }

    [Fact]
    public void GeneratePassword_ExcludeSimilarChars_DoesNotContainSimilarCharacters()
    {
        // Arrange
        var options = new PasswordGenerationOptions(
       Length: 100, // Longer to test thoroughly
  ExcludeSimilarChars: true
      );

        // Act
    var result = _service.GeneratePassword(options);

   // Assert - should not contain i, l, 1, L, o, 0, O
        var similarChars = new[] { 'i', 'l', '1', 'L', 'o', '0', 'O' };
        foreach (var c in similarChars)
     {
            Assert.DoesNotContain(c, result.Password);
        }
    }

    [Fact]
    public void GeneratePassword_MultipleGenerations_ProducesDifferentPasswords()
    {
     // Arrange
     var options = new PasswordGenerationOptions();

        // Act
        var password1 = _service.GeneratePassword(options).Password;
  var password2 = _service.GeneratePassword(options).Password;
        var password3 = _service.GeneratePassword(options).Password;

        // Assert
      Assert.NotEqual(password1, password2);
     Assert.NotEqual(password2, password3);
        Assert.NotEqual(password1, password3);
    }

    [Fact]
    public void GeneratePassword_EstimatesStrengthCorrectly()
    {
    // Arrange
        var weakOptions = new PasswordGenerationOptions(
            Length: 8,
      IncludeUppercase: false,
         IncludeLowercase: true,
            IncludeNumbers: false,
            IncludeSpecialChars: false
    );

        var strongOptions = new PasswordGenerationOptions(
            Length: 24,
  IncludeUppercase: true,
   IncludeLowercase: true,
  IncludeNumbers: true,
IncludeSpecialChars: true
   );

        // Act
        var weakResult = _service.GeneratePassword(weakOptions);
        var strongResult = _service.GeneratePassword(strongOptions);

      // Assert
        Assert.True(strongResult.EstimatedStrength > weakResult.EstimatedStrength,
            $"Strong password strength ({strongResult.EstimatedStrength}) should be greater than weak password strength ({weakResult.EstimatedStrength})");
    }

    [Fact]
    public void GeneratePassword_IncludesCorrectCriteria()
    {
        // Arrange
        var options = new PasswordGenerationOptions(
     Length: 16,
        IncludeUppercase: true,
            IncludeLowercase: true,
            IncludeNumbers: false,
            IncludeSpecialChars: false
  );

        // Act
  var result = _service.GeneratePassword(options);

// Assert
        Assert.Contains("Uppercase letters", result.UsedCriteria);
      Assert.Contains("Lowercase letters", result.UsedCriteria);
        Assert.DoesNotContain("Numbers", result.UsedCriteria);
        Assert.DoesNotContain("Special characters", result.UsedCriteria);
    }

    [Theory]
    [InlineData(8, true)]  // Short password should have tip about length
    [InlineData(16, false)] // Longer password should not have length tip
    public void GeneratePassword_ProvidesAppropriateSecurityTip(int length, bool expectLengthTip)
    {
  // Arrange
  var options = new PasswordGenerationOptions(
            Length: length,
      IncludeSpecialChars: true
        );

        // Act
        var result = _service.GeneratePassword(options);

        // Assert
     Assert.NotEmpty(result.SecurityTip);
        if (expectLengthTip)
        {
       Assert.Contains("longer", result.SecurityTip.ToLower());
        }
    }

    [Fact]
    public void ValidateOptions_WithValidOptions_ReturnsTrue()
    {
    // Arrange
      var options = new PasswordGenerationOptions();

        // Act
        var isValid = _service.ValidateOptions(options, out var errorMessage);

        // Assert
        Assert.True(isValid);
  Assert.Empty(errorMessage);
    }

    [Fact]
    public void ValidateOptions_WithTooShortLength_ReturnsFalse()
    {
  // Arrange
     var options = new PasswordGenerationOptions(Length: 3);

        // Act
 var isValid = _service.ValidateOptions(options, out var errorMessage);

        // Assert
        Assert.False(isValid);
        Assert.Contains("at least 4", errorMessage);
    }

    [Fact]
    public void ValidateOptions_WithTooLongLength_ReturnsFalse()
    {
    // Arrange
    var options = new PasswordGenerationOptions(Length: 200);

        // Act
      var isValid = _service.ValidateOptions(options, out var errorMessage);

        // Assert
Assert.False(isValid);
        Assert.Contains("cannot exceed 128", errorMessage);
    }

    [Fact]
public void ValidateOptions_WithNoCharacterTypes_ReturnsFalse()
    {
        // Arrange
        var options = new PasswordGenerationOptions(
 IncludeUppercase: false,
 IncludeLowercase: false,
            IncludeNumbers: false,
         IncludeSpecialChars: false
   );

        // Act
        var isValid = _service.ValidateOptions(options, out var errorMessage);

        // Assert
        Assert.False(isValid);
        Assert.Contains("At least one character type", errorMessage);
    }

    [Fact]
    public void GeneratePassword_NoCharacterTypesSelected_ThrowsException()
    {
        // Arrange
        var options = new PasswordGenerationOptions(
      IncludeUppercase: false,
     IncludeLowercase: false,
       IncludeNumbers: false,
        IncludeSpecialChars: false
   );

        // Act & Assert
     Assert.Throws<ArgumentException>(() => _service.GeneratePassword(options));
    }
}
