using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePropertyPro.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TenantIdentifier
    {
        private readonly RequestDelegate _next;

        public TenantIdentifier(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, GlobalDbContext globalDbContext)
        {
            var tenantId = httpContext.Request.Headers["X-Tenant-Id"].FirstOrDefault();

            if(!string.IsNullOrWhiteSpace(tenantId))
            {
                var tenant = await globalDbContext.Tenants.SingleOrDefaultAsync(x => x.Id.ToString() == tenantId);
                httpContext.Items["TENANT"] = tenant;
            }

            await _next.Invoke(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TenantIdentifierExtensions
    {
        public static IApplicationBuilder UseTenantIdentifier(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantIdentifier>();
        }
    }
}
