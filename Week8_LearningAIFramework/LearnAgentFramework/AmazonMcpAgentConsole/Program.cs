using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Microsoft.Agents.AI;
using OpenAI.Responses;
using System.Net;
using System.Reflection;

const string AgentName = "AmazonSearchAgent";
const string AgentInstructions = "You are a helpful shopping assistant that can search for products on Amazon. " +
                                 "Use the available Amazon Product Search MCP tools to help users find products, compare prices, " +
                                 "and make informed purchasing decisions.";
const string Model = "gpt-4o";

var mcpTool = new MCPToolDefinition(
    serverLabel: "smithery_amazon_product_search",
    serverUrl: "https://server.smithery.ai/@SiliconValleyInsight/amazon-product-search/mcp?api_key=e8ba3983-2b5c-466d-82b3-43ccfe119113&profile=complicated-tapir-CVSm0s");
mcpTool.AllowedTools.Add("find_products_to_buy");
mcpTool.AllowedTools.Add("shop_for_items");

var smitheryApiKey = Environment.GetEnvironmentVariable("SMITHERY_API_KEY");

RunToolResources toolResources = new()
{
    Mcp = new Dictionary<string, McpServerToolResource>
    {
        { mcpServerLabel, new McpServerToolResource
            {
                Headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {smitheryApiKey}" },
                    // or if they use a different header:
                    // { "X-API-Key", smitheryApiKey }
                }
            }
        }
    }
};

var persistentAgentsClient = new PersistentAgentsClient(
    "https://dj-persistent-agent-resource.services.ai.azure.com/api/projects/dj-persistent-agent"
    ,
    new DefaultAzureCredential(new DefaultAzureCredentialOptions()
    {
        ExcludeAzureCliCredential = true,
        ExcludeAzurePowerShellCredential = true,
        ExcludeEnvironmentCredential = true,
        ExcludeInteractiveBrowserCredential = true
    }));

var agentMetadata = await persistentAgentsClient.Administration.CreateAgentAsync(
    model: Model,
    name: AgentName,
    instructions: AgentInstructions,
    tools: [mcpTool]);

AIAgent agent = await persistentAgentsClient.GetAIAgentAsync(agentMetadata.Value.Id);

var runOptions = new ChatClientAgentRunOptions()
{
    ChatOptions = new()
    {
        RawRepresentationFactory = (_) => new ThreadAndRunOptions()
        {
            ToolResources = new MCPToolResource(serverLabel: "smithery_amazon_product_search")
            {
                RequireApproval = new MCPApproval("Always"),
            }.ToToolResources()
        }
    }
};

AgentThread thread = agent.GetNewThread();
var response = await agent.RunAsync(
    "My laptop is always overheating. Are there any products that can help me with that?",
    thread,
    runOptions);
Console.WriteLine(response);

