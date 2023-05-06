using DataAccess;
using FluentValidation;
using MinimalAPI.Demo.V6.Extensions;

namespace MinimalAPI.Demo.V6.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.name).NotEmpty();
        RuleFor(p => p.age).GreaterThan(0).LessThan(200);
    }
}
