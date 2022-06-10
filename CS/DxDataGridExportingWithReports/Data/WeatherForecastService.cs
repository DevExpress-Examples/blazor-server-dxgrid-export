namespace DxDataGridExportingWithReports.Data {
    public class WeatherForecastService {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        WeatherForecast[]? Forecasts;
        public Task<WeatherForecast[]> GetForecastAsync() {
            if (Forecasts == null) {
                var rng = new Random();
                Forecasts = Enumerable.Range(1, 40).Select(index => new WeatherForecast {
                    Date = DateTime.Today.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToArray();
            }
            return Task.FromResult(Forecasts);
        }
    }
}
