using Hangfire.Dashboard;

namespace AspNetCorePropertyPro.Api.Extensions
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}