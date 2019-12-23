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

        public DbSet<Property> Properties { get; set; }
        public DbSet <Favourite> Favourites { get; set; }
        public DbSet <Flag> Flags { get; set; }
        public DbSet <PropertyType> PropertyTypes { get; set; }
        public DbSet <DealType> DealTypes { get; set; }
        public DbSet <PropertyImage> PropertyImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new DealTypeConfiguration());
            builder.ApplyConfiguration(new FavouriteConfiguration());
            builder.ApplyConfiguration(new FlagConfiguration());
            builder.ApplyConfiguration(new PropertTypeConfiguration());
            builder.ApplyConfiguration(new PropertyConfiguration());
            builder.ApplyConfiguration(new PropertyImageConfiguration());
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = @"server=(localdb)\mssqllocaldb; database=pptenantone; integrated security=true";
            optionsBuilder.UseSqlServer(conn);
            //optionsBuilder.UseSqlServer(_tenant.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}