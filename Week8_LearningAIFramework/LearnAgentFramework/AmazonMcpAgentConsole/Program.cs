using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Microsoft.Agents.AI;
using OpenAI.Responses;
using System.Net;
using System.Reflection;

const string AgentName = "AmazonSearchAgent";
const string AgentInstructions = "You are a helpful shopping assistant that can search for products on Amazon. " +
                                 "Use the available Amazon Product Search tools to help users find products, compare prices, " +
                                 "and make informed purchasing decisions.";
const string Model = "gpt-4o";

// Try both local and hosted MCP servers
var mcpServers = new[]
{
    new { Label = "local_amazon", Url = "http://localhost:3000", Description = "Local Amazon MCP Server" },
    new { Label = "smithery_amazon", Url = "https://server.smithery.ai/@SiliconValleyInsight/amazon-product-search/mcp?api_key=e8ba3983-2b5c-466d-82b3-43ccfe119113&profile=complicated-tapir-CVSm0s", Description = "Hosted Smithery Amazon MCP Server" },
    new { Label = "test_server", Url = "http://localhost:8080", Description = "Test MCP Server" }
};

string? workingUrl = null;
string? workingLabel = null;

Console.WriteLine("Searching for available MCP servers...");
using var httpClient = new HttpClient();
httpClient.Timeout = TimeSpan.FromSeconds(10);

foreach (var server in mcpServers)
{
    try
    {
        Console.WriteLine($"Testing {server.Description} at {server.Url}...");
        
        var testResponse = await httpClient.GetAsync(server.Url);
        Console.WriteLine($"✓ Found server: {server.Description} - Status: {testResponse.StatusCode}");
        workingUrl = server.Url;
        workingLabel = server.Label;
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ {server.Description} not available: {ex.Message}");
    }
}

if (workingUrl == null)
{
    Console.WriteLine("\n❌ No MCP server found.");
    Console.WriteLine("\nOptions to try:");
    Console.WriteLine("1. Use the working Smithery server (requires internet)");
    Console.WriteLine("2. Set up local Amazon MCP server");
    Console.WriteLine("3. Use a simple test MCP server");
    
    // Fall back to the Smithery server since it was working before
    Console.WriteLine("\n🔄 Attempting to use Smithery server anyway...");
    workingUrl = mcpServers[1].Url;
    workingLabel = mcpServers[1].Label;
}

Console.WriteLine($"\n✅ Using MCP server: {workingUrl}");

var mcpTool = new MCPToolDefinition(
    serverLabel: workingLabel ?? "amazon_search",
    serverUrl: workingUrl);

// Add some common Amazon search tools
mcpTool.AllowedTools.Add("search_products");
mcpTool.AllowedTools.Add("find_products");
mcpTool.AllowedTools.Add("product_search");

var persistentAgentsClient = new PersistentAgentsClient(
    "https://dj-persistent-agent-resource.services.ai.azure.com/api/projects/dj-persistent-agent",
    new DefaultAzureCredential(new DefaultAzureCredentialOptions()
    {
        ExcludeAzureCliCredential = true,
        ExcludeAzurePowerShellCredential = true,
        ExcludeEnvironmentCredential = true,
        ExcludeInteractiveBrowserCredential = true
    }));

try
{
    Console.WriteLine("Creating Amazon search agent...");
    var agentMetadata = await persistentAgentsClient.Administration.CreateAgentAsync(
        model: Model,
        name: AgentName,
        instructions: AgentInstructions,
        tools: [mcpTool]);

    Console.WriteLine($"Agent created: {agentMetadata.Value.Id}");

    AIAgent agent = await persistentAgentsClient.GetAIAgentAsync(agentMetadata.Value.Id);

    var runOptions = new ChatClientAgentRunOptions()
    {
        ChatOptions = new()
        {
            RawRepresentationFactory = (_) => new ThreadAndRunOptions()
            {
                ToolResources = new MCPToolResource(serverLabel: workingLabel ?? "amazon_search")
                {
                    RequireApproval = new MCPApproval("never"),
                }.ToToolResources()
            }
        }
    };

    AgentThread thread = agent.GetNewThread();
    Console.WriteLine("Sending query to agent...");
    
    var response = await agent.RunAsync(
        "My laptop is always overheating. Are there any products that can help me with that?",
        thread,
        runOptions);
    
    Console.WriteLine($"Response: {response}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
    }
    
    Console.WriteLine("\n🔍 Troubleshooting suggestions:");
    Console.WriteLine("1. Check if the MCP server URL is correct");
    Console.WriteLine("2. Verify the server label matches what the server expects");
    Console.WriteLine("3. Ensure the tool names are correct for the MCP server");
    Console.WriteLine("4. Check network connectivity to the MCP server");
}

