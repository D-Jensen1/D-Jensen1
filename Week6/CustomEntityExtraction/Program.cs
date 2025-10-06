using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using System.Net;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string endpoint = config["endpoint"];
string key = config["key"];
string projectName = "CustomEntityLab";
string deploymentName = "AdEntities";
var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));


var results = await client.RecognizeCustomEntitiesAsync(WaitUntil.Completed,
    Directory.GetFiles(@"ads\").Select(fileName => File.ReadAllText(fileName)).ToList()
    ,projectName
    ,deploymentName
    );

foreach (var item in results.GetValues())
{
    foreach (var doc in item)
    {
        Console.WriteLine($"=========document id:{doc.Id}=========");
        foreach (var entity in doc.Entities)
        {
            Console.WriteLine($"{entity.Category}:{entity.Text}");
        }
    }
} 
