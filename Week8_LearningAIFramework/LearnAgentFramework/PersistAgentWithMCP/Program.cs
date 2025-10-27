using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Microsoft.Agents.AI;
using OpenAI.Responses;
using System.Net;
using System.Reflection;

const string AgentName = "ShoppingAssistant";
const string AgentInstructions = "You are a helpful shopping assistant. When users ask about products or shopping, " +
                                 "try to provide helpful advice about what to look for and suggest they search on shopping websites.";
const string Model = "gpt-4o";

// Since local MCP servers are having issues, let's use the working Microsoft Learn server
// and adjust the agent to be helpful for shopping queries without requiring Amazon API
var mcpTool = new MCPToolDefinition(
    serverLabel: "microsoft_learn",
    serverUrl: "https://learn.microsoft.com/api/mcp");
mcpTool.AllowedTools.Add("microsoft_docs_search");

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
    Console.WriteLine("Creating shopping assistant agent...");
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
                ToolResources = new MCPToolResource(serverLabel: "microsoft_learn")
                {
                    RequireApproval = new MCPApproval("never"),
                }.ToToolResources()
            }
        }
    };

    AgentThread thread = agent.GetNewThread();
    Console.WriteLine("Sending query to agent...");
    
    var response = await agent.RunAsync(
        "My laptop is always overheating. What should I look for in laptop cooling solutions? What are the different types available?",
        thread,
        runOptions);
    
    Console.WriteLine($"Response: {response}");

    Console.WriteLine("\n" + "=".PadRight(50, '='));
    Console.WriteLine("Follow-up question:");
    
    var response2 = await agent.RunAsync(
        "Can you search Microsoft Learn for information about managing system performance and cooling?",
        thread,
        runOptions);
    
    Console.WriteLine($"Response: {response2}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
    }
}

