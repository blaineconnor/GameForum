using Game.Forum.Application.Models.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Game.Forum.Application.Validators
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.
                    Where(p => p.Value.ValidationState == ModelValidationState.Invalid).
                    ToDictionary(p => p.Key, p => p.Value.Errors.Select(e => e.ErrorMessage)).ToArray();

                context.Result = new BadRequestObjectResult(CustomResponseDto.Fail(errors, HttpStatusCode.NotAcceptable));
                return;

            }
            await next();

        }
    }
}
