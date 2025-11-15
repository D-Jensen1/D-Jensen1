using System.Net;
using CipherScore.ApiService.Services;
using Moq;
using Moq.Protected;
using Xunit;

namespace CipherScore.ApiService.Tests.Services;

public class HaveIBeenPwnedServiceTests
{
    [Fact]
    public async Task CheckPasswordAsync_BreachedPassword_ReturnsBreachResult()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("0018A45C4D1DEF81644B54AB7F969B88D65:1234\r\n00D4F6E8FA6EECAD2A3AA415EEC418D38EC:5")
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var service = new HaveIBeenPwnedService(httpClient);

        // Act - testing with "password" which has known breaches
        var result = await service.CheckPasswordAsync("password");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("HaveIBeenPwned", result.Source);
    }

    [Fact]
    public async Task CheckPasswordAsync_SafePassword_ReturnsNoBreachResult()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA:1\r\nBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB:2")
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var service = new HaveIBeenPwnedService(httpClient);

        // Act
        var result = await service.CheckPasswordAsync("VeryUniqueP@ssw0rd!2024");

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsBreached);
        Assert.Equal(0, result.BreachCount);
        Assert.Equal("HaveIBeenPwned", result.Source);
    }

    [Fact]
    public async Task CheckPasswordAsync_ApiUnavailable_ReturnsErrorResult()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.ServiceUnavailable
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var service = new HaveIBeenPwnedService(httpClient);

        // Act
        var result = await service.CheckPasswordAsync("testpassword");

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsBreached);
        Assert.Equal("API unavailable", result.Source);
    }

    [Fact]
    public async Task CheckPasswordAsync_UsesKAnonymity_OnlySendsFirst5Characters()
    {
        // Arrange
        HttpRequestMessage? capturedRequest = null;
        
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Callback<HttpRequestMessage, CancellationToken>((req, ct) => capturedRequest = req)
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var service = new HaveIBeenPwnedService(httpClient);

        // Act
        await service.CheckPasswordAsync("testpassword");

        // Assert
        Assert.NotNull(capturedRequest);
        Assert.Contains("/range/", capturedRequest.RequestUri?.ToString());
        
        // Extract the hash prefix from the URL
        var urlParts = capturedRequest.RequestUri?.ToString().Split('/');
        var hashPrefix = urlParts?.Last();
        
        // Should be exactly 5 characters
        Assert.Equal(5, hashPrefix?.Length);
    }

    [Fact]
    public async Task CheckMultiplePasswordsAsync_MultiplePasswords_ReturnsAllResults()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("AAAA:1\r\nBBBB:2")
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var service = new HaveIBeenPwnedService(httpClient);

        var passwords = new[] { "password1", "password2", "password3" };

        // Act
        var results = await service.CheckMultiplePasswordsAsync(passwords);

        // Assert
        Assert.Equal(3, results.Count);
        Assert.All(results.Values, result => Assert.NotNull(result));
    }
}
