using Azure.Data.Tables;
using CipherScore.ApiService.Services;
using CipherScore.Shared.DTOs.Requests;
using CipherScore.Shared.DTOs.Responses;
using CipherScore.Shared.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CipherScore.ApiService.Services.AIService>();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Register HttpClient for HaveIBeenPwned service
builder.Services.AddHttpClient<HaveIBeenPwnedService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10); // Set reasonable timeout
});


// Register password-related servicesS
builder.Services.AddSingleton<PasswordStrengthService>();
builder.Services.AddSingleton<PasswordGeneratorService>();
builder.Services.AddScoped<PasswordSecurityService>();
builder.Services.AddSingleton<PasswordHistoryService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Azure Table Storage client
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config["AzureStorage:ConnectionString"];
    var tableName = "PasswordEntries";
    return new TableClient(connectionString, tableName);
});
builder.Services.AddControllers();

// Register Azure Table Storage service
builder.Services.AddSingleton<PasswordStorageService>();

var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Password Analysis API endpoints
app.MapPost("/api/password/analyze", (PasswordAnalysisRequest request, PasswordStrengthService strengthService, PasswordHistoryService historyService) =>
{
    var analysis = strengthService.AnalyzePassword(request.Password);
    
    // Save to history
    historyService.SaveAnalysisHistory(request.Password, analysis, request.UserId);
    
    return Results.Ok(analysis);
})
.WithName("AnalyzePassword")
.WithSummary("Analyze password strength and save to history");

// Enhanced breach check endpoint using HaveIBeenPwned
app.MapPost("/api/password/breach-check", async (PasswordBreachRequest request, PasswordSecurityService securityService) =>
{
    var result = await securityService.CheckPasswordBreachAsync(request.Password);
    return Results.Ok(result);
})
.WithName("CheckPasswordBreachReal")
.WithSummary("Check if password appears in known breaches using HaveIBeenPwned API");

// Legacy breach check endpoint (for hash-based checking)
app.MapGet("/api/password/breach-check/{passwordHash}", (string passwordHash, PasswordSecurityService securityService) =>
{
    var result = securityService.CheckPasswordBreach(passwordHash);
    return Results.Ok(result);
})
.WithName("CheckPasswordBreach")
.WithSummary("Check if password hash appears in known breaches (simulated)");

app.MapGet("/api/password/common-passwords", (int limit, PasswordSecurityService securityService) =>
{
    var commonPasswords = securityService.GetCommonPasswords(limit);
    return Results.Ok(commonPasswords);
})
.WithName("GetCommonPasswords")
.WithSummary("Get list of common passwords to avoid");

app.MapPost("/api/password/history", (PasswordAnalysisRequest request, PasswordStrengthService strengthService, PasswordHistoryService historyService) =>
{
    var analysis = strengthService.AnalyzePassword(request.Password);
    var historyEntry = historyService.SaveAnalysisHistory(request.Password, analysis, request.UserId);
    return Results.Ok(historyEntry);
})
.WithName("SavePasswordHistory")
.WithSummary("Save password analysis to history");

app.MapGet("/api/password/history", (int limit, PasswordHistoryService historyService) =>
{
    var history = historyService.GetAnalysisHistory(limit);
    return Results.Ok(history);
})
.WithName("GetPasswordHistory")
.WithSummary("Get password analysis history");

app.MapGet("/api/password/stats", (PasswordHistoryService historyService) =>
{
    var stats = historyService.GetPasswordStats();
    return Results.Ok(stats);
})
.WithName("GetPasswordStats")
.WithSummary("Get password strength statistics and trends");

app.MapPost("/api/password/generate", (PasswordGenerationOptions options, PasswordSecurityService securityService) =>
{
    var suggestion = securityService.GeneratePasswordSuggestion(options);
    return Results.Ok(suggestion);
})
.WithName("GeneratePassword")
.WithSummary("Generate secure password suggestion");

// Comprehensive password security check endpoint
app.MapPost("/api/password/security-check", async (PasswordSecurityCheckRequest request, PasswordStrengthService strengthService, PasswordSecurityService securityService, PasswordHistoryService historyService) =>
{
    var analysis = strengthService.AnalyzePassword(request.Password);
    var breachCheck = await securityService.CheckPasswordBreachAsync(request.Password);
    
    // Save to history if requested
    if (request.SaveToHistory)
    {
        historyService.SaveAnalysisHistory(request.Password, analysis, request.UserId);
    }
    
    var result = new PasswordSecurityCheckResult(
        StrengthAnalysis: analysis,
        BreachStatus: breachCheck,
        IsSecure: analysis.Score >= 60 && !breachCheck.IsBreached,
        SecurityRecommendations: GetSecurityRecommendations(analysis, breachCheck)
    );
    
    return Results.Ok(result);
})
.WithName("ComprehensiveSecurityCheck")
.WithSummary("Comprehensive password security analysis including strength and breach checking");

// NEW: Submit password to Azure Table Storage
app.MapPost("/api/password/submit", async (PasswordSubmissionRequest request, PasswordStrengthService strengthService, PasswordSecurityService securityService, PasswordStorageService storageService) =>
{
    var analysis = strengthService.AnalyzePassword(request.Password);
    var breachCheck = await securityService.CheckPasswordBreachAsync(request.Password);
    
    var entry = await storageService.SavePasswordEntryAsync(
        request.Password,
        analysis,
        breachCheck,
        request.UserGuess
    );
    
    return Results.Ok(new PasswordSubmissionResult(
        Message: "Password submitted successfully",
        EntryId: entry.RowKey,
        StrengthRating: entry.StrengthRating,
        BruteForceTime: entry.ActualBruteForceTime,
        GuessAccuracy: entry.GuessAccuracy,
        IsBreached: breachCheck.IsBreached,
        Analysis: analysis
    ));
})
.WithName("SubmitPassword")
.WithSummary("Submit password for analysis and store in Azure Table Storage");

// NEW: Get stored password entries
app.MapGet("/api/password/submissions", async (int limit, PasswordStorageService storageService) =>
{
    var entries = await storageService.GetRecentEntriesAsync(limit);
    return Results.Ok(entries);
})
.WithName("GetSubmissions")
.WithSummary("Get recent password submissions from Azure Table Storage");

// NEW: Get storage statistics
app.MapGet("/api/password/storage-stats", async (PasswordStorageService storageService) =>
{
    var stats = await storageService.GetStorageStatsAsync();
    return Results.Ok(stats);
})
.WithName("GetStorageStats")
.WithSummary("Get statistics from stored password submissions");

app.MapDefaultEndpoints();

app.Run();

// Helper method for security recommendations
static string[] GetSecurityRecommendations(PasswordAnalysisResult analysis, BreachCheckResult breachCheck)
{
    var recommendations = new List<string>();
    
    if (breachCheck.IsBreached)
    {
        recommendations.Add($"⚠ This password has been found in {breachCheck.BreachCount} data breaches. Change it immediately!");
    }
    
    if (analysis.Score < 60)
    {
        recommendations.Add("⚠ Consider using a stronger password with more complexity.");
    }
    
    recommendations.AddRange(analysis.Suggestions);
    
    if (!recommendations.Any())
    {
        recommendations.Add("✓ This password appears to be secure!");
    }
    
    return recommendations.ToArray();
}