using AspNetCorePropertyPro.Core.Services;
using AspNetCorePropertyPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class HangfireRecurringJobService : IHangfireRecurringJobService
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly TenantDbContext tenantDbContext;

        public HangfireRecurringJobService(IEmailService emailService, IUserService userService, TenantDbContext tenantDbContext)
        {
            _emailService = emailService;
            _userService = userService;
            this.tenantDbContext = tenantDbContext;
        }
        public async Task CheckForAbandonedCart(string tenant)
        {
            TenantDbContext.connStr = tenant;
            var users = await _userService.GetAllUsers();

            if (users.Any())
                foreach (var user in users)
                    await _emailService.SendMailAsync(user.Email, "HangFire", $"Testing hangfire {tenant}");
        }
    }
}
