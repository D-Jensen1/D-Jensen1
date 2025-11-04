var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
//https://learn.microsoft.com/en-us/dotnet/aspire/caching/stackexchange-redis-integration?tabs=dotnet-cli&pivots=redis

var apiService = builder.AddProject<Projects.AspireQuickStartApp_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.AspireQuickStartApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
