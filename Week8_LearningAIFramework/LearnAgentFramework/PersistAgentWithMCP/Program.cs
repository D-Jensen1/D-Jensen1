using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Microsoft.Agents.AI;
using OpenAI.Responses;
using System.Net;
using System.Reflection;

const string AgentName = "MicrosoftLearnAgent";
const string AgentInstructions = "You answer questions by searching the Microsoft Learn content only.";
const string Model = "gpt-4o";

var mcpTool = new MCPToolDefinition(
    serverLabel: "microsoft_learn",
    serverUrl: "https://learn.microsoft.com/api/mcp");
mcpTool.AllowedTools.Add("microsoft_docs_search");


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
            ToolResources = new MCPToolResource(serverLabel: "microsoft_learn")
            {
                RequireApproval = new MCPApproval("never"),
            }.ToToolResources()
        }
    }
};

AgentThread thread = agent.GetNewThread();
var response = await agent.RunAsync(
    "Please summarize the Azure AI Agent documentation related to MCP Tool calling.",
    thread,
    runOptions);
Console.WriteLine(response);

