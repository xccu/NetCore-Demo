using System.Globalization;
using System.Reflection;

namespace MinimalAPI.Demo.V6.Bindings;

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    #region simply control
    public static bool TryParse(string? value, IFormatProvider? provider, out Location? location)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var values = value.Split(',');

            if (values.Length == 2 &&
                double.TryParse(values[0], out var latitude) &&
                double.TryParse(values[1], out var longitude))
            {
                location = new Location
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
                return true;
            }
        }
        location = null;
        return false;
    }
    #endregion



}