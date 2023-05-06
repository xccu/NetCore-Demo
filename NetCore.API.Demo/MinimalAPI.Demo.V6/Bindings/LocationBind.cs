using System.Reflection;

namespace MinimalAPI.Demo.V6.Bindings;

public class LocationBind
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    #region completely control
    public static ValueTask<LocationBind?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (double.TryParse(context.Request.Query["lat"], out var latitude) &&
            double.TryParse(context.Request.Query["lon"], out var longitude))
        {
            var location = new LocationBind { Latitude = latitude, Longitude = longitude };
            return ValueTask.FromResult<LocationBind?>(location);
        }
        return ValueTask.FromResult<LocationBind?>(null);
    }
    #endregion
}
