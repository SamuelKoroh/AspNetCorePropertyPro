using AspNetCorePropertyPro.Core.Services;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void InitializeTenants(this IServiceCollection services, ITenantService tenantService)
        {
             //var tenantService = app.ApplicationServices.GetRequiredService <ITenantService>();
            var tenants = tenantService.GetTenants().Result;

            foreach (var tenant in tenants)
            {
                services.AddHangfire(x => x.UseSqlServerStorage(tenant.ConnectionString));
                services.AddHangfireServer();
            }

        }
    }
}
