using Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Demo.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet(Name = "GetWeatherForecast")]
    [Permission("AtLeast18")]
    public IEnumerable<WeatherForecast> Get()
    {
        if (!User.Identity.IsAuthenticated)
            return null;
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "PostWeatherForecast")]
    //[Permission("AtLeast18")]
    public IActionResult Post(WeatherForecast weather)
    {
        return CreatedAtAction(nameof(Get), weather);
    }
}