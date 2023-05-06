using Common.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Common.ModelBinder;

public class LocationBinder : IModelBinder
{

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelName = bindingContext.ModelName;

        // Try to fetch the value of the argument by name
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;
        var values = value.Split(',');

        if (values.Length == 2 &&
                        double.TryParse(values[0], out var latitude) &&
                        double.TryParse(values[1], out var longitude))
        {
            var location = new Location
            {
                Latitude = latitude,
                Longitude = longitude
            };
            bindingContext.Result = ModelBindingResult.Success(location);
            return Task.CompletedTask;
        }

        bindingContext.ModelState.TryAddModelError(modelName, "values error");
        return Task.CompletedTask;
    }
          
}
