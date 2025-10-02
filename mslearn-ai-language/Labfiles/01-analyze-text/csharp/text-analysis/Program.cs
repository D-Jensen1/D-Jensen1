using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;



var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

string endpoint = config["Endpoint"];
string key = config["Key"];

var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));

Response<DetectedLanguage> result = client.DetectLanguage(File.ReadAllText(@"reviews\review1.txt"));
Console.WriteLine($"Language detected is: {result.Value.Name}, confidence: {result.Value.ConfidenceScore:P0}\n");

Response<DocumentSentiment> resultSentiment = client.AnalyzeSentiment(File.ReadAllText(@"reviews\review5.txt"));
Console.WriteLine($"Sentiment is: {resultSentiment.Value.Sentiment}");
                  
Console.WriteLine($"Positive Sentiment Confidence: {resultSentiment.Value.ConfidenceScores.Positive:P0} | " +
                  $"Neutral Sentiment Confidence: {resultSentiment.Value.ConfidenceScores.Neutral:P0} | " +
                  $"Negative Sentiment Confidence: {resultSentiment.Value.ConfidenceScores.Negative:P0}\n");

Response<KeyPhraseCollection> resultPhrases = client.ExtractKeyPhrases(File.ReadAllText(@"reviews\review1.txt"));
var resultPhrasesList = resultPhrases.Value.ToList();
resultPhrasesList.Sort();
Console.WriteLine($"Key Phrases: ");
int counter = 0;
foreach (var phrase in resultPhrasesList)
{
    if (counter == 4)
    {
        Console.WriteLine(phrase);
        counter = 0;
    }
    else
    {
        Console.Write($"{phrase} | ");
    }
    counter++;
}

Response<CategorizedEntityCollection> resultEntities = client.RecognizeEntities(File.ReadAllText(@"reviews\review3.txt"));
Console.WriteLine($"\nEntities: ");

// Find a way to group the categories and sub-categories
foreach (var entity in resultEntities.Value.ToList())
{
    Console.WriteLine($"{entity.Text} | Category: {entity.Category} | Confidence: {entity.ConfidenceScore:P0}");
}

Response<LinkedEntityCollection> resultLinkedEntities = client.RecognizeLinkedEntities(File.ReadAllText(@"reviews\review3.txt"));
Console.WriteLine($"\nLinked Entities: ");

// Find a way to group the categories and sub-categories
foreach (var entity in resultLinkedEntities.Value.ToList())
{
    Console.WriteLine($"{entity.Name}: {entity.Url}");
}
