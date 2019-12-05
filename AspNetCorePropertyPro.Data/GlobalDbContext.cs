using AspNetCorePropertyPro.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePropertyPro.Data
{
    public class GlobalDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) 
            : base(options){}

    }
}