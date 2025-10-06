using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Data;



var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

string endpoint = config["endpoint"];
string key = config["key"];
string filePath = @"images\image-46.jpg";
ImageAnalysisClient client = new ImageAnalysisClient(
    new Uri(endpoint),
    new AzureKeyCredential(key));

using FileStream stream = new FileStream(filePath, FileMode.Open);

// Get the smart-cropped thumbnails for the image.
ImageAnalysisResult result = client.Analyze(
    BinaryData.FromStream(stream),
    VisualFeatures.SmartCrops,
    new ImageAnalysisOptions { SmartCropsAspectRatios = new float[] { 1f } });//width/height ratio
stream.Position = 0;
// Print smart-crops analysis results to the console
Console.WriteLine($"Image analysis results:");
Console.WriteLine($" Metadata: Model: {result.ModelVersion} Image dimensions: {result.Metadata.Width} x {result.Metadata.Height}");
Console.WriteLine($" SmartCrops:");
foreach (CropRegion cropRegion in result.SmartCrops.Values)
{
    var box = cropRegion.BoundingBox;
    Console.WriteLine($"   Aspect ratio: {cropRegion.AspectRatio}, Bounding box: {box}");
    var source = Image.Load(stream);
    source.Mutate(ctx=>ctx.Crop(new Rectangle(box.X, box.Y,box.Width,box.Height)));
    source.SaveAsJpeg(@"images\cropped-image-46.jpg");
}