using Microsoft.Extensions.Configuration;
using MultiTurnConversations;

// Configuration setup
var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>().Build();

// Create and run the console application
var app = new ConsoleApplication(config);
await app.RunAsync();