using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Micro.Abstractions.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Micro.API.Validations;

public class UseCustomErrorModelInterceptor : IValidatorInterceptor
{
    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
    {
        return commonContext;
    }

    public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext,
        ValidationResult result)
    {
        var failures = result.Errors
            .Select(error => new ValidationFailure(error.PropertyName, SerializeError(error)));

        return new ValidationResult(failures);
    }

    private static string SerializeError(ValidationFailure failure)
    {
        var error = new Error(failure.ErrorCode, failure.ErrorMessage);
        return JsonSerializer.Serialize(error);
    }
}
