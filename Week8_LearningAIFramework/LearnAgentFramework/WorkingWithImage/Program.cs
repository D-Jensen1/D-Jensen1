using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System.ClientModel;
using System.IO;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>().Build();

AIAgent agent = new AzureOpenAIClient(
  new Uri(config["endpoint"]),
  new ApiKeyCredential(config["key"]))
    .GetChatClient(config["model"])
    .CreateAIAgent(
        name: "VisionAgent",
        instructions: "You are a helpful agent that can analyze images and emotion and evaluating " +
                      "the age of people. When listing the files reference (if analyzing multple images) " +
                      "you should use the name of the file as it is stored.");

ChatMessage message1 = new(ChatRole.User, [
    new TextContent("What do you see in this image?"),
    new UriContent("https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Gfp-wisconsin-madison-the-nature-boardwalk.jpg/2560px-Gfp-wisconsin-madison-the-nature-boardwalk.jpg", "image/jpeg")
]);
//Console.WriteLine(await agent.RunAsync(message1));
// can we upload local image?

ChatMessage message2 = new(ChatRole.User, [
    new TextContent("What do you see in this image?"),
    new DataContent(File.ReadAllBytes(@"..\..\..\Images\image2.jpg"), "image/jpg")
    ]);
//Console.WriteLine(await agent.RunAsync(message2));

// can we do mulitple images?        
ChatMessage message3 = new(ChatRole.User, [
    new TextContent("What do you see in these images?"),
    new DataContent(File.ReadAllBytes(@"..\..\..\Images\image2.jpg"), "image/jpg"),
    new DataContent(File.ReadAllBytes(@"..\..\..\Images\image1.webp"), "image/webp")

    ]);
Console.WriteLine(await agent.RunAsync(message3));

// can we do animated gif?
// will only analyze first frame of gif
ChatMessage message4 = new(ChatRole.User, [
    new TextContent("What do you see in these images?"),
    new DataContent(File.ReadAllBytes(@"..\..\..\Images\image4.gif"), "image/gif")
    ]);
//Console.WriteLine(await agent.RunAsync(message4));


// read a bunch of local files
List<AIContent> messageContents = new();
messageContents.Add(new TextContent("What do you see in these images, describe each picture individually in 30 words or less? Also determine the primary colors in the pictures."));
foreach (var file in Directory.GetFiles(@"E:\repos\D-Jensen1\Week8_LearningAIFramework\LearnAgentFramework\WorkingWithImage\Images", @" *.jpg"))
{
    messageContents.Add(new TextContent($"The next file is named {file}."));
    messageContents.Add(new DataContent(File.ReadAllBytes(file), "image/jpg"));
}

ChatMessage message5 = new(ChatRole.User, messageContents);
await foreach (var msg in agent.RunStreamingAsync(message5))
{
    Console.Write(msg);
}

// how much pain is harold hiding?
var haroldMessage = new ChatMessage(ChatRole.User, [
    new TextContent("Analyse emotion, age, expression,description in plain english only"),
    new DataContent(File.ReadAllBytes(@"..\..\..\Images\image3.webp"),"image/webp")
    ]);

//await foreach (var msg in agent.RunStreamingAsync(haroldMessage))
//{
//    Console.Write(msg);
//}

