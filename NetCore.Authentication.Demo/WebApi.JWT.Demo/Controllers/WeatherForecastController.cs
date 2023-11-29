using Security;
using Microsoft.AspNetCore.Mvc;
using WebApi.JWT.Demo;

namespace WebAPI.Demo.Controllers;

[ApiController]
[Route("api/weather")]
//[Permission("AtLeast18")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Permission("AtLeast18")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }

    [HttpPost]
    //[Permission("AtLeast18")]
    public IActionResult Post(WeatherForecast weather)
    {
        return CreatedAtAction(nameof(Get), weather);
    }
}