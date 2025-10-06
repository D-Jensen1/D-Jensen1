using Azure.AI.OpenAI;
using Azure.Identity;
using OpenAI.Images;
using System.ClientModel;


string endpoint = "https://vc-mg8bctjv-swedencentral.cognitiveservices.azure.com/";
string deployment ="dall-e-3";

DefaultAzureCredential credential = new DefaultAzureCredential();
AzureOpenAIClient azureClient = new AzureOpenAIClient(new Uri(endpoint), credential);

ImageClient client = azureClient.GetImageClient(deployment);

Task<ClientResult<GeneratedImage>> imageResultTask1 = client.GenerateImageAsync("3 little piggy dancing", new()
{
    Quality = GeneratedImageQuality.Standard,
    Size = GeneratedImageSize.W1024xH1024,
    Style = GeneratedImageStyle.Vivid,
    ResponseFormat = GeneratedImageFormat.Uri
});
Task<ClientResult<GeneratedImage>> imageResultTask2 = client.GenerateImageAsync("2 little piggy dancing", new()
{
    Quality = GeneratedImageQuality.Standard,
    Size = GeneratedImageSize.W1024xH1024,
    Style = GeneratedImageStyle.Vivid,
    ResponseFormat = GeneratedImageFormat.Uri
});
Task<ClientResult<GeneratedImage>> imageResultTask3 = client.GenerateImageAsync("1 little piggy dancing", new()
{
    Quality = GeneratedImageQuality.Standard,
    Size = GeneratedImageSize.W1024xH1024,
    Style = GeneratedImageStyle.Vivid,
    ResponseFormat = GeneratedImageFormat.Uri
});

List<Task<ClientResult<GeneratedImage>>> tasks = [imageResultTask1, imageResultTask2, imageResultTask3];
while (!tasks.All(t=>t.IsCompleted))
{
    await Task.Delay(2000);
    Console.Write(".");
}
Console.WriteLine("all done.");
foreach (var item in tasks)
{
    var imageResult = item.Result;
    GeneratedImage image = imageResult.Value;
    Console.WriteLine(image.ImageUri);
    HttpClient client1 = new();

    Stream imageStream = await client1.GetStreamAsync(image.ImageUri);
    FileStream file = new FileStream($"image-{Guid.NewGuid()}.png", FileMode.Create);

    Task writingOutputFile = imageStream.CopyToAsync(file);
    Console.WriteLine($"Writing to file {file.Name}");
    while (!writingOutputFile.IsCompleted)
    {
        Console.WriteLine($"{file.Position} bytes");
        await Task.Delay(500);
    }
    Console.WriteLine("done.");
    file.Close();

}


