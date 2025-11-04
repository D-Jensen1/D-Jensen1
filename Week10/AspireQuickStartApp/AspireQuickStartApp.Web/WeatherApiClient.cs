namespace AspireQuickStartApp.Web;

public class WeatherApiClient(HttpClient httpClient) : IWeatherApiClient //default contructor
{
    public async Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<WeatherForecast>? forecasts = null;

        await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast", cancellationToken))
        {
            if (forecasts?.Count >= maxItems)
            {
                break;
            }
            if (forecast is not null)
            {
                forecasts ??= [];
                forecasts.Add(forecast);
            }
        }

        return forecasts?.ToArray() ?? [];
    } 
}
public class FakeWeatherApiClient(HttpClient httpClient) : IWeatherApiClient //default constructor
{
    public async Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<WeatherForecast>? forecasts = [];

        forecasts.Add(new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 30, "Sunny"));

        return forecasts?.ToArray() ?? [];
    }
}
public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public interface IWeatherApiClient
{
    Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default);
}