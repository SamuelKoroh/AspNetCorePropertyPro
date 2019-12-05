using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AspNetCorePropertyPro.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AspNetCorePropertyPro.Data.EntityConfiguration;

namespace AspNetCorePropertyPro.Data
{
    public class TenantDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly Tenant _tenant;
        public TenantDbContext(DbContextOptions<TenantDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            if (httpContextAccessor.HttpContext != null)
                _tenant = (Tenant)httpContextAccessor.HttpContext.Items["TENANT"];
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = "Server=(localDB)\\MSSqlLocalDB; database=PPTenantThree; Integrated Security=true";
            optionsBuilder.UseSqlServer(conn);
            // optionsBuilder.UseSqlServer(_tenant.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}