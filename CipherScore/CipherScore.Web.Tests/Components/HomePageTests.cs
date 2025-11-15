using Bunit;
using CipherScore.Web.Components.Pages;
using CipherScore.ApiService.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace CipherScore.Web.Tests.Components;

public class HomePageTests : TestContext
{
    private readonly PasswordStrengthService _passwordService;
    private readonly Mock<CipherApiClient> _mockApiClient;

    public HomePageTests()
    {
        _passwordService = new PasswordStrengthService(); // Use real service instead of mock
        _mockApiClient = new Mock<CipherApiClient>(Mock.Of<HttpClient>());
        
        Services.AddSingleton(_passwordService);
        Services.AddSingleton(_mockApiClient.Object);
    }

    [Fact]
    public void HomePage_RendersCorrectly()
    {
        // Act
        var cut = RenderComponent<Home>();

        // Assert
        Assert.Contains("CipherScore", cut.Markup);
        Assert.Contains("Enter Your Password", cut.Markup);
    }

    [Fact]
    public void HomePage_InitialState_ShowsEmptyState()
    {
        // Act
        var cut = RenderComponent<Home>();

        // Assert
        Assert.Contains("Enter a password above to see its strength analysis", cut.Markup);
    }

    [Fact]
    public void HomePage_PasswordInput_TriggersAnalysis()
    {
        // Arrange
        var cut = RenderComponent<Home>();
        var input = cut.Find("#passwordInput");

        // Act
        input.Change("TestPassword123!");

        // Assert - verify the component state has updated
        // Since we're using the real service, we can check that analysis happened
        Assert.DoesNotContain("Enter a password above to see its strength analysis", cut.Markup);
    }

    [Fact]
    public void HomePage_TogglePasswordVisibility_ChangesInputType()
    {
        // Arrange
        var cut = RenderComponent<Home>();
        var input = cut.Find("#passwordInput");
        var toggleButton = cut.Find(".btn-outline-secondary");

        // Assert initial state (password hidden)
        Assert.Contains("type=\"password\"", input.ToMarkup());

        // Act - click toggle button
        toggleButton.Click();

        // Assert - password visible
        input = cut.Find("#passwordInput");
        Assert.Contains("type=\"text\"", input.ToMarkup());

        // Act - click toggle button again
        toggleButton.Click();

        // Assert - password hidden again
        input = cut.Find("#passwordInput");
        Assert.Contains("type=\"password\"", input.ToMarkup());
    }

    [Fact]
    public void HomePage_ClearButton_ResetsForm()
    {
        // Arrange
        var cut = RenderComponent<Home>();
        var input = cut.Find("#passwordInput");
        input.Change("TestPassword");

        var clearButton = cut.FindAll("button").First(b => b.TextContent.Contains("Clear"));

        // Act
        clearButton.Click();

        // Assert
        input = cut.Find("#passwordInput");
        Assert.Empty(input.GetAttribute("value") ?? "");
        Assert.Contains("Enter a password above", cut.Markup);
    }
}
