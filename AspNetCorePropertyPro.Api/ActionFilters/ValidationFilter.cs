using AspNetCorePropertyPro.Api.Resources.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.ActionFilters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.ErrorCount > 0)
            {
                var errorInModelState = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorInModelState)
                    foreach (var subKey in error.Value)
                        errorResponse.Errors.Add(
                          new ErrorModel
                          {
                              FieldName = error.Key,
                              Message = subKey
                          });
                context.Result = new BadRequestObjectResult(errorResponse);
            }
            await next();

        }
    }
}
