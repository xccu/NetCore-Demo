using Common.ModelBinder;
using Microsoft.AspNetCore.Mvc;

namespace Common.Model;

[ModelBinder(BinderType = typeof(LocationBinder))]
public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
