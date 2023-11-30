namespace BlazorApp.Data
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecastVm>> Get();
    }
}
