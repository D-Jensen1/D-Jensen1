using CipherScore.Web;
using CipherScore.Web.Components;
using CipherScore.ApiService;
using CipherScore.ApiService.Services;
using Polly;
using Polly.Timeout;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register password strength service
builder.Services.AddScoped<PasswordStrengthService>();


builder.Services.AddServiceDiscovery(options =>
{
    options.RefreshPeriod = TimeSpan.FromSeconds(90);
});


// Override Aspire's default HTTP resilience policies
builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddStandardResilienceHandler(options =>
    {
        // Timeout settings
        options.TotalRequestTimeout.Timeout = TimeSpan.FromMinutes(10);
        options.AttemptTimeout.Timeout = TimeSpan.FromMinutes(10);

        // Circuit breaker needs to be at least 2x the attempt timeout
        options.CircuitBreaker.SamplingDuration = TimeSpan.FromMinutes(20);
    });

 
    http.AddServiceDiscovery();
    http.ConfigureHttpClient(client =>
    {
        client.Timeout = TimeSpan.FromMinutes(10);
    });
});



// Register CipherApiClient for API communication
builder.Services.AddHttpClient<CipherApiClient>(client =>
    {
       
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
        client.Timeout = Timeout.InfiniteTimeSpan;
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(10),
            ConnectTimeout = TimeSpan.FromMinutes(2),
            KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
        };
    }); 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
