using System.ComponentModel.DataAnnotations;

namespace Blazor.WebAssembly.V10.Demo;
public interface IValidationService
{
    Task<bool> ValidateModelAsync(object model);
    List<ValidationResult> GetValidationResults(object model);
}

public class ValidationService : IValidationService
{
    public async Task<bool> ValidateModelAsync(object model)
    {
        // 模拟异步验证过程
        await Task.Delay(10);

        var results = GetValidationResults(model);
        return results.Count == 0;
    }

    public List<ValidationResult> GetValidationResults(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);

        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}