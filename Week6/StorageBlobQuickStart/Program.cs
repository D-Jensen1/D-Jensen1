//Create BlobServiceClient using UseDevelopmentStorage=true as connection string
// Retrieve the connection string for use with the application. 
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

////use project secret:
//var config = new ConfigurationBuilder()
//    .AddUserSecrets<Program>().Build();
//string connectionString = config["StorageConnString"];


//use emulator
//string connectionString = "UseDevelopmentStorage=true";

// Create a BlobServiceClient object 
//var blobServiceClient = new BlobServiceClient(connectionString);
var token = new InteractiveBrowserCredential();

var blobServiceClient = new BlobServiceClient(new Uri("https://vc0316.blob.core.windows.net"), token);
//use ServiceClient to create a container
//Create a unique name for the container
string containerName = "vc-quickstartblobs" + Guid.NewGuid().ToString();

BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

//use container to upload a blob
string localPath = "data";
Directory.CreateDirectory(localPath);
string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
string localFilePath = Path.Combine(localPath, fileName);

await File.WriteAllTextAsync(localFilePath, "Hello, World!");

// Get a reference to a blob
BlobClient blobClient = containerClient.GetBlobClient(fileName);

Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

// Upload data from the local file, overwrite the blob if it already exists
await blobClient.UploadAsync(localFilePath, true);

//list blobs
Console.WriteLine("Listing blobs...");

// List all blobs in the container
await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
{
    Console.WriteLine("\t" + blobItem.Name);
}


// download a blob
// Download the blob to a local file
// Append the string "DOWNLOADED" before the .txt extension 
// so you can compare the files in the data directory
string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

// Download the blob's contents and save it to a file
await blobClient.DownloadToAsync(downloadFilePath);

// delete container.

// Clean up
Console.Write("Press any key to begin clean up");
Console.ReadLine();

Console.WriteLine("Deleting blob container...");
await containerClient.DeleteAsync();

Console.WriteLine("Deleting the local source and downloaded files...");
File.Delete(localFilePath);
File.Delete(downloadFilePath);

Console.WriteLine("Done");