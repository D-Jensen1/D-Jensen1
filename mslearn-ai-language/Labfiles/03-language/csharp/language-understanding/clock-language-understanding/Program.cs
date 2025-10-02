using Azure;
using Azure.AI.Language.Conversations;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using System.Text.Json;


var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string endpoint = config["endpoint"];
string key = config["key"];
string projectName = "Clock";
string deploymentName = "production";
var client = new ConversationAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));

Console.WriteLine("Enter time related inquiry:");
string userInput = Console.ReadLine();

var data = new
{
    analysisInput = new
    {
        conversationItem = new
        {
            text = userInput,
            id = "1",
            participantId = "1",
        }
    },
    parameters = new
    {
        projectName,
        deploymentName,

        // Use Utf16CodeUnit for strings in .NET.
        stringIndexType = "Utf16CodeUnit",
    },
    kind = "Conversation",
};

Response response = client.AnalyzeConversation(RequestContent.Create(data));

using JsonDocument result = JsonDocument.Parse(response.ContentStream);
JsonElement conversationalTaskResult = result.RootElement;
JsonElement conversationPrediction = conversationalTaskResult.GetProperty("result").GetProperty("prediction");

Console.WriteLine($"Top intent: {conversationPrediction.GetProperty("topIntent").GetString()}");

Console.WriteLine("Intents:");
foreach (JsonElement intent in conversationPrediction.GetProperty("intents").EnumerateArray())
{
    Console.WriteLine($"Category: {intent.GetProperty("category").GetString()}");
    Console.WriteLine($"Confidence: {intent.GetProperty("confidenceScore").GetSingle()}");
    Console.WriteLine();
}

Console.WriteLine("Entities:");
foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
{
    Console.WriteLine($"Category: {entity.GetProperty("category").GetString()}");
    Console.WriteLine($"Text: {entity.GetProperty("text").GetString()}");
    Console.WriteLine($"Offset: {entity.GetProperty("offset").GetInt32()}");
    Console.WriteLine($"Length: {entity.GetProperty("length").GetInt32()}");
    Console.WriteLine($"Confidence: {entity.GetProperty("confidenceScore").GetSingle()}");
    Console.WriteLine();

    if (entity.TryGetProperty("resolutions", out JsonElement resolutions))
    {
        foreach (JsonElement resolution in resolutions.EnumerateArray())
        {
            if (resolution.GetProperty("resolutionKind").GetString() == "DateTimeResolution")
            {
                Console.WriteLine($"Datetime Sub Kind: {resolution.GetProperty("dateTimeSubKind").GetString()}");
                Console.WriteLine($"Timex: {resolution.GetProperty("timex").GetString()}");
                Console.WriteLine($"Value: {resolution.GetProperty("value").GetString()}");
                Console.WriteLine();
            }
        }
    }
}