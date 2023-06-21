using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Foundation.API.Types;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.API.Extensions
{
    public class FluentValidatorInterceptorExtension : IValidatorInterceptor
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
            if (!Enum.TryParse(failure.ErrorCode, out HttpStatusCode httpStatusCode))
                return JsonSerializer.Serialize(new Error());
            
            var httpError = new Error(httpStatusCode, (int)httpStatusCode, failure.ErrorMessage);
            return JsonSerializer.Serialize(httpError);
        }
    }
}