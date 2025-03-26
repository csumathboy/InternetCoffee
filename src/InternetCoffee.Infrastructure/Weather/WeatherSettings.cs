namespace csumathboy.Infrastructure.Caching;

public class WeatherSettings
{
    public string WeatherApiKey { get; set; } = default!;
    public string WeatherBaseUrl { get; set; } = default!;
    public string LocationApiKey { get; set; } = default!;
    public string LocationBaseUrl { get; set; } = default!;
}