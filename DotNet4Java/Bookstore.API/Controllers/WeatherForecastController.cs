using Bookstore.API.Filters;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ConsoleActionFilter("Weather controller", Order = 5)]
    [ConsoleAsyncActionFilterAtrribute("Async Weather controller", Order = 109)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;
        private readonly BookstoreConfiguration _bookstoreConfiguration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration,
            IOptions<BookstoreConfiguration> bookstoreOptions)
        {
            _logger = logger;
            this._configuration = configuration;
            _bookstoreConfiguration = bookstoreOptions.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [ConsoleActionFilter("Weather GET Method", Order = 5)]
        [ConsoleAsyncActionFilterAtrribute("Async Weather GET Method")]
        public IEnumerable<WeatherForecast> Get()
        {


            var properties = typeof(Book).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach(var property in properties)
            {
                var attributes = property.GetCustomAttributes(true);
                
                foreach(var attribute in attributes)
                {
                    if(attribute is BookstoreAttribute bookstoreAttribute)
                    {
                        Console.WriteLine($"Property name {property.Name}, meta name {bookstoreAttribute.Name}");
                    }
                }
            }


            var maximumBooks = _configuration.GetSection("Bookstore").GetValue(typeof(int),"MaximumBooks");

            var bookstoreConfiguration = _configuration.GetSection("Bookstore").Get<BookstoreConfiguration>();

            Console.WriteLine(_bookstoreConfiguration.MaximumBooks);

            _logger.LogInformation("This is the get method of Weather forecast");

            var weathers = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            _logger.LogInformation("These are the weathers: {Weathers}", weathers);

            return weathers;
        }
    }
}