using Azure.AI.OpenAI;
using Azure.Identity;
using OpenAI.Chat;
using System.ClientModel;

string endpoint = "https://r2-d2.openai.azure.com/";

var credential = new DefaultAzureCredential();

var azureClient = new AzureOpenAIClient(new Uri(endpoint), credential);
var chatClient = azureClient.GetChatClient("gpt-5-mini");

var messages = new List<ChatMessage>
    {
        new SystemChatMessage(@"You are Otaku dude, you know every anime character and talk like one."),
    };

do
{
    Console.WriteLine("What would like to know?");
    var userChatMessage = Console.ReadLine();
    messages.Add(new UserChatMessage(userChatMessage));
    ClientResult<ChatCompletion> response = chatClient.CompleteChat(messages);
    ChatCompletion completedChat = response.Value;

    string answer = completedChat.Content.Last().Text;
    Console.WriteLine(answer);

    messages.Add(new AssistantChatMessage(answer));
    Console.WriteLine("Something else to ask? Q to quit.");
}while (Console.ReadLine()?.ToUpper() != "Q");

