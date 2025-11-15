using System.Net;
using System.Net.Http.Json;
using CipherScore.Shared.DTOs.Responses;
using CipherScore.Shared.Models;
using CipherScore.Web;
using Moq;
using Moq.Protected;
using Xunit;

namespace CipherScore.Web.Tests.Services;

public class CipherApiClientTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly CipherApiClient _client;

    public CipherApiClientTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://localhost")
        };
        _client = new CipherApiClient(_httpClient);
    }

    [Fact]
    public async Task AnalyzePasswordAsync_ValidPassword_ReturnsAnalysisResult()
    {
        // Arrange
        var expectedResult = new PasswordAnalysisResult
        {
            Score = 80,
            Strengths = new List<string> { "Strong password" },
            Suggestions = new List<string>()
        };

        SetupHttpResponse(HttpStatusCode.OK, expectedResult);

        // Act
        var result = await _client.AnalyzePasswordAsync("TestPassword123!");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(80, result.Score);
    }

    [Fact]
    public async Task CheckPasswordBreachAsync_BreachedPassword_ReturnsBreachResult()
    {
        // Arrange
        var expectedResult = new BreachCheckResult(true, 12345, null, "HaveIBeenPwned");

        SetupHttpResponse(HttpStatusCode.OK, expectedResult);

        // Act
        var result = await _client.CheckPasswordBreachAsync("password");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsBreached);
        Assert.Equal(12345, result.BreachCount);
    }

    [Fact]
    public async Task CheckPasswordSecurityAsync_ValidPassword_ReturnsSecurityCheckResult()
    {
        // Arrange
        var expectedResult = new PasswordSecurityCheckResult(
            new PasswordAnalysisResult { Score = 75 },
            new BreachCheckResult(false, 0),
            true,
            new[] { "Password is secure" }
        );

        SetupHttpResponse(HttpStatusCode.OK, expectedResult);

        // Act
        var result = await _client.CheckPasswordSecurityAsync("SecureP@ssw0rd!");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSecure);
        Assert.False(result.BreachStatus.IsBreached);
    }

    [Fact]
    public async Task GetCommonPasswordsAsync_ReturnsPasswordArray()
    {
        // Arrange
        var expectedResult = new[] { "password", "123456", "qwerty" };

        SetupHttpResponse(HttpStatusCode.OK, expectedResult);

        // Act
        var result = await _client.GetCommonPasswordsAsync(3);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Length);
        Assert.Contains("password", result);
    }

    [Fact]
    public async Task SubmitPasswordAsync_ValidSubmission_ReturnsSubmissionResult()
    {
        // Arrange
        var expectedResult = new PasswordSubmissionResult(
            "Success",
            "12345",
            "Strong",
            "10 years",
            "Accurate",
            false,
            new PasswordAnalysisResult()
        );

        SetupHttpResponse(HttpStatusCode.OK, expectedResult);

        // Act
        var result = await _client.SubmitPasswordAsync("TestPassword", "Strong");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Success", result.Message);
        Assert.Equal("12345", result.EntryId);
    }

    [Fact]
    public async Task AnalyzePasswordAsync_HttpError_ThrowsException()
    {
        // Arrange
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            () => _client.AnalyzePasswordAsync("test")
        );
    }

    private void SetupHttpResponse<T>(HttpStatusCode statusCode, T content)
    {
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = JsonContent.Create(content)
            });
    }
}
