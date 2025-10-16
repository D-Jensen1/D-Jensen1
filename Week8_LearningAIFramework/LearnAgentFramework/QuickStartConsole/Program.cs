using Azure;
using Azure.AI.Agents.Persistent;
using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI;
using System.ClientModel;
using System.Threading;


var persistentAgentsClient = new PersistentAgentsClient(
    "https://dj-persistent-agent-resource.services.ai.azure.com/api/projects/dj-persistent-agent",
    new DefaultAzureCredential(new DefaultAzureCredentialOptions()
    {
        ExcludeAzureCliCredential=true,
        ExcludeAzurePowerShellCredential=true,
        ExcludeEnvironmentCredential=true,
        ExcludeInteractiveBrowserCredential=true
    })
    );


Console.WriteLine("List of agents: ");

var agents = persistentAgentsClient.Administration.GetAgents().ToList();
string agentID = string.Empty;
AIAgent basicChatAgent = null;

while (agents.Count > 0)
{
    // Display current agents
    for (int i = 0; i < agents.Count; i++)
    {
        Console.WriteLine($"{i+1}-{agents[i].Id}\t{agents[i].Name}\t{agents[i].Model}\tCreated={agents[i].CreatedAt:u}");
    }
    Console.WriteLine("Enter 'd' to delete agents, 's' to select agent, or any other key to use default agent.");

    var userSelection = Console.ReadLine();
    if (userSelection == "d")
    {
        Console.WriteLine("Enter number to delete specific agent, 'all' to delete all, or 'done' to continue:");
        string input = Console.ReadLine();

        if (input.ToLower() == "all")
        {
            foreach (var agent in agents)
            {
                persistentAgentsClient.Administration.DeleteAgent(agent.Id);
            }
            agents.Clear();
            Console.WriteLine("All agents deleted.");
            break;
        }
        else if (input.ToLower() == "done")
        {
            break;
        }
        else if (int.TryParse(input, out int selectedIndex) && selectedIndex >= 0 && selectedIndex < agents.Count)
        {
            var agentToDelete = agents[selectedIndex];
            persistentAgentsClient.Administration.DeleteAgent(agentToDelete.Id);
            agents.RemoveAt(selectedIndex);
            Console.WriteLine($"Agent {agentToDelete.Name} deleted.");
        }
        else
        {
            Console.WriteLine("Invalid input. Continuing with existing agents.");
            break;
        }
    }
    else if(userSelection == "s")
    {
        Console.WriteLine($"Which Agent would you like to use? (enter 1-{agents.Count})");
        string agentInput = Console.ReadLine();
        int agentSelected;

        while (!int.TryParse(agentInput, out agentSelected) || agentSelected < 1 || agentSelected > agents.Count)
        {
            Console.WriteLine($"Invalid input. Please enter a number between 1 and {agents.Count}:");
            agentInput = Console.ReadLine();
        }

        // Convert from 1-based user input to 0-based array index
        agentSelected = agentSelected - 1;
        agentID = agents[agentSelected].Id;
        basicChatAgent = await persistentAgentsClient.GetAIAgentAsync(agentID);

        Console.Clear();
        Console.WriteLine($"Now using {basicChatAgent.Name} | {basicChatAgent.Id}");
        break;
    }
    
}


if (agents.Count == 0)
{
    // No agents left, create a new one
    basicChatAgent = await persistentAgentsClient.CreateAIAgentAsync(
        model: "gpt-5-mini",
        name: "Joker",
        instructions: "You are good at telling jokes."
    );
}


var thread = persistentAgentsClient.Threads;
//foreach (var test in thread)
//{

//}
//var thread = basicChatAgent.GetNewThread("<threadID>")

//AIAgent agent = await persistentAgentsClient.GetAIAgentAsync("asst_EZCOF7PCwlw90iRtZMAHS1IA");

Console.WriteLine("What can I assist with today?");

string userInput = Console.ReadLine();
while (userInput != "q")
{

    if (!string.IsNullOrWhiteSpace(userInput))
    {
        await foreach (var update in basicChatAgent.RunStreamingAsync(userInput))
        {
            Console.Write(update);
        }
    }

    Console.WriteLine("\n");
    userInput = Console.ReadLine();
}

/*Console.WriteLine(await agent.RunAsync("Tell me a joke about a pirate."));

await foreach (var update in agent.RunStreamingAsync("Tell me a joke about a pirate."))
{
    Console.WriteLine(update);
}

ChatMessage message = new(ChatRole.User, [
    new TextContent("Tell me a joke about this image?"),
    new UriContent("https://upload.wikimedia.org/wikipedia/commons/1/11/Joseph_Grimaldi.jpg", "image/jpeg")
]);

Console.WriteLine(await agent.RunAsync(message));

ChatMessage systemMessage = new(
    ChatRole.System,
    """
    If the user asks you to tell a joke, refuse to do so, explaining that you are not a clown.
    Offer the user an interesting fact instead.
    """);
ChatMessage userMessage = new(ChatRole.User, "Tell me a joke about a pirate.");

Console.WriteLine(await agent.RunAsync([systemMessage, userMessage]));*/