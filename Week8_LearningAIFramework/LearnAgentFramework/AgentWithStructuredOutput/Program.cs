using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using OpenAI;
using OpenAI.Chat;
using HtmlAgilityPack;

var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT is not set.");
var deploymentName = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME") ?? "gpt-4o-mini";

// Create chat client to be used by chat client agents.
ChatClient chatClient = new AzureOpenAIClient(
    new Uri(endpoint),
    new AzureCliCredential())
        .GetChatClient(deploymentName);

// Scrape content from the Wikiversity page
string pageContent = await ScrapeWikiversityPage("https://en.wikiversity.org/wiki/Educational_Media_Awareness_Campaign");

// Create the ChatClientAgent with the specified name and instructions.
ChatClientAgent agent = chatClient.CreateAIAgent(new ChatClientAgentOptions(name: "WebAnalyst", instructions: "You are a web page analysis assistant that extracts key information from web pages."));

// Set WebPageInfo as the type parameter of RunAsync method to specify the expected structured output from the agent and invoke the agent with the scraped content.
AgentRunResponse<WebPageInfo> response = await agent.RunAsync<WebPageInfo>($"Please analyze this web page content: {pageContent}");

// Access the structured output via the Result property of the agent response.
Console.WriteLine("Web Page Analysis Output:");
Console.WriteLine($"Title: {response.Result.Title}");
Console.WriteLine($"Description: {response.Result.Description}");
Console.WriteLine($"URL: {response.Result.Url}");
Console.WriteLine($"Main Topic: {response.Result.MainTopic}");
Console.WriteLine($"Keywords: {string.Join(", ", response.Result.Keywords ?? new string[0])}");
Console.WriteLine($"Contact Email: {response.Result.ContactEmail}");

// Create the ChatClientAgent with the specified name, instructions, and expected structured output the agent should produce.
ChatClientAgent agentWithWebPageInfo = chatClient.CreateAIAgent(new ChatClientAgentOptions(name: "WebAnalyst", instructions: "You are a web page analysis assistant that extracts key information from web pages.")
{
    ChatOptions = new()
    {
        ResponseFormat = Microsoft.Extensions.AI.ChatResponseFormat.ForJsonSchema<WebPageInfo>()
    }
});

// Invoke the agent with the scraped content while streaming, to extract the structured information from.
var updates = agentWithWebPageInfo.RunStreamingAsync($"Please analyze this web page content: {pageContent}");

// Assemble all the parts of the streamed output, since we can only deserialize once we have the full json,
// then deserialize the response into the WebPageInfo class.
WebPageInfo webPageInfo = (await updates.ToAgentRunResponseAsync()).Deserialize<WebPageInfo>(JsonSerializerOptions.Web);

Console.WriteLine("\nStreaming Web Page Analysis Output:");
Console.WriteLine($"Title: {webPageInfo.Title}");
Console.WriteLine($"Description: {webPageInfo.Description}");
Console.WriteLine($"URL: {webPageInfo.Url}");
Console.WriteLine($"Main Topic: {webPageInfo.MainTopic}");
Console.WriteLine($"Keywords: {string.Join(", ", webPageInfo.Keywords ?? new string[0])}");
Console.WriteLine($"Contact Email: {webPageInfo.ContactEmail}");

static async Task<string> ScrapeWikiversityPage(string url)
{
    using var httpClient = new HttpClient();
    
    // Set a user agent to avoid being blocked
    httpClient.DefaultRequestHeaders.Add("User-Agent", 
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
    
    try
    {
        string html = await httpClient.GetStringAsync(url);
        
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        // Extract the title
        var titleNode = doc.DocumentNode.SelectSingleNode("//title");
        string title = titleNode?.InnerText?.Trim() ?? "No title found";
        
        // Extract the main content from the mw-parser-output div
        var contentNode = doc.DocumentNode.SelectSingleNode("//div[@class='mw-parser-output']");
        
        if (contentNode == null)
        {
            return $"Title: {title}\nContent: Unable to extract main content from the page.";
        }
        
        // Remove script and style elements
        var scriptsAndStyles = contentNode.SelectNodes(".//script | .//style");
        if (scriptsAndStyles != null)
        {
            foreach (var node in scriptsAndStyles)
            {
                node.Remove();
            }
        }
        
        // Extract text content and clean it up
        string content = contentNode.InnerText;
        
        // Clean up the text by removing extra whitespace and newlines
        content = System.Text.RegularExpressions.Regex.Replace(content, @"\s+", " ").Trim();
        
        // Limit content length to avoid token limits (keeping first 2000 characters)
        if (content.Length > 2000)
        {
            content = content.Substring(0, 2000) + "...";
        }
        
        return $"URL: {url}\nTitle: {title}\nContent: {content}";
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error scraping page: {ex.Message}");
        return $"URL: {url}\nError: Unable to scrape content from the page. {ex.Message}";
    }
}

[Description("Information about a web page including title, description, URL, main topic, keywords, and contact information")]
public class WebPageInfo
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("main_topic")]
    public string? MainTopic { get; set; }

    [JsonPropertyName("keywords")]
    public string[]? Keywords { get; set; }

    [JsonPropertyName("contact_email")]
    public string? ContactEmail { get; set; }
}