using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.ActionFilters
{
    public class TenantHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name ="X-Tenant-Id",
                Description = "Tenant Id, Type: GUID (e.g b0ed668d-7ef2-4a23-a333-94ad278f45d7)",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema{
                   Type ="string",
                   Default = new OpenApiString("b0ed668d-7ef2-4a23-a333-94ad278f45d7")
                }
            });
        }
    }
}
