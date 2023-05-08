using DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Demo.V6.Bindings;
using MiniValidation;
using System.ComponentModel.DataAnnotations;
using System;
using System.Security.Claims;
using System.Xml.Linq;

namespace MinimalAPI.Demo.V6.Extensions;

public static class ValidationAPIExtension
{
    public static WebApplication UseValidationAPI(this WebApplication app)
    {
        app.MapPost("/validation/mini", MiniValidation);
        app.MapPost("/validation/fluent",  FluentValidation);
        app.MapPost("/validation/fluentAsync", FluentValidationAsync);

        return app;
    }

    #region using MiniValidator to valitade
    static IResult MiniValidation(User user) 
    {
        var isValid = MiniValidator.TryValidate(user, out var errors);
        if (!isValid)
        {
            return Results.ValidationProblem(errors);
        }
        return Results.NoContent();
    }
    #endregion

    #region using FluentValidation to valitade
    static IResult FluentValidation(User user, IValidator<User> validator)
    {
        var validationResult = validator.Validate(user);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            return Results.ValidationProblem(errors);
        }
        return Results.NoContent();
    }

    static async Task<IResult> FluentValidationAsync(User user, IValidator<User> validator) 
    {
        var validationResult = await validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            return Results.ValidationProblem(errors);
        }
        return Results.NoContent();
    }
    #endregion
}