using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System.ClientModel;
using System.ComponentModel;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>().Build();

[Description("This is a reference for some of my favorite things.")]
static string MyFavorites()
    => "Color: red. Movie: The Bee Movie. Drink: Sweet Tea";

AIAgent agent = new AzureOpenAIClient(
  new Uri(config["endpoint"]),
  new ApiKeyCredential(config["key"]))
    .GetChatClient(config["model"])
    .CreateAIAgent(instructions: "You are a helpful everyday assistand. " +
                                 "After each response ask the user a question and remind them they can end the program by pressing q then enter.",
                   tools: [AIFunctionFactory.Create(MyFavorites)]);

AgentThread thread = agent.GetNewThread();

Console.WriteLine("What can I assist with today?");

string userInput = Console.ReadLine();
while (userInput != "q")
{
    
    if (!string.IsNullOrWhiteSpace(userInput))
    {
        await foreach (var update in agent.RunStreamingAsync(userInput, thread))
        {
            Console.Write(update);
        }
    }
    
    Console.WriteLine("\n");
    userInput = Console.ReadLine();
}


