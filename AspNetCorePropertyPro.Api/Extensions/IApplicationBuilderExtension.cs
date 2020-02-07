using AspNetCorePropertyPro.Core.Services;
using AspNetCorePropertyPro.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static void InitializeTenants(this IApplicationBuilder app, ITenantService tenantService)
        {
             //var teervice = app.ApplicationServices.GetRequiredService <ITenantService>();
            var tenants = tenantService.GetTenants().Result;

            foreach (var tenant in tenants)
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage(tenant.ConnectionString);
                var sqlStorage = new SqlServerStorage(tenant.ConnectionString);

                //var myapp = app.New();
                app.UseHangfireDashboard($"/dashboard/{tenant.Name}", new DashboardOptions
                {
                    Authorization = new[] { new HangfireAuthFilter() }

                }, sqlStorage);

                var options = new BackgroundJobServerOptions
                {
                    ServerName = string.Format("{0}.{1}", tenant.Name, Guid.NewGuid().ToString()),
                };

                var jobStorage = JobStorage.Current;
                
                app.UseHangfireServer(options, null,jobStorage);



                //RecurringJob.AddOrUpdate<IHangfireRecurringJobService>(j => j.CheckForAbandonedCart(), Cron.Minutely);
                var recurringJobManager = new RecurringJobManager();
                recurringJobManager.RemoveIfExists(tenant.Id.ToString());
                recurringJobManager.AddOrUpdate<IHangfireRecurringJobService>(tenant.Id.ToString(), j => j.CheckForAbandonedCart(tenant.ConnectionString), Cron.Minutely);
                recurringJobManager.Trigger(tenant.Id.ToString());
            }

        }
    }
}
