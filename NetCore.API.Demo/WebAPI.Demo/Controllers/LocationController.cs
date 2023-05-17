using Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Demo.Controllers;

[ApiController]
public class LocationController : ControllerBase
{
    //http://localhost:5163/navigate?location=43.8427,7.8527
    [HttpGet("navigate")]
    public IActionResult Navigate([ModelBinder(Name = "location")] Location location)
    {
        try
        {
            return Ok($"Location: {location.Latitude}, {location.Longitude}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("navigate2")]
    public IActionResult NavigatePost([ModelBinder(Name = "location")] Location location)
    {
        try
        {
            return Ok($"Location: {location.Latitude}, {location.Longitude}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
