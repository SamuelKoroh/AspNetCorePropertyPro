using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Data.Repositories
{
    public class PropertyImageRepository : Repository<PropertyImage>, IPropertyImageRepository
    {
        public PropertyImageRepository(TenantDbContext context) : base(context)
        {
        }

        private TenantDbContext TenantDbContext
        {
            get { return Context as TenantDbContext; }
        }
    }
}
