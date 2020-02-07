using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Data.Repositories
{
    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        public TenantRepository(GlobalDbContext context) : base(context)
        {
        }

        public GlobalDbContext GlobalDbContext
        {
            get
            {
                return Context as GlobalDbContext;
            }
        }
    }
}
