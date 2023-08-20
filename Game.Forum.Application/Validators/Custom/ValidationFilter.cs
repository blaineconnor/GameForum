using Game.Forum.Application.Models.RequestModels.Custom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Game.Forum.Application.Validators.Custom
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

                context.Result = new BadRequestObjectResult(ResponseVM.Fail(errors, HttpStatusCode.NotAcceptable));
                return;

            }
            await next();

        }
    }
}
