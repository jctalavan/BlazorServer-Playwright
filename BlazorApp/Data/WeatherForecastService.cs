namespace BlazorApp.Data
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherForecastVm>> Get()
        {
            return await _httpClient
                .GetFromJsonAsync<WeatherForecastVm[]>("/WeatherForecast");
        }
    }
}