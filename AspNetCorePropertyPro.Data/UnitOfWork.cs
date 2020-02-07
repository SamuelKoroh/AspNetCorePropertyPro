using AspNetCorePropertyPro.Core;
using AspNetCorePropertyPro.Core.Repositories;
using AspNetCorePropertyPro.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TenantDbContext _context;
        private readonly GlobalDbContext _globalDbContext;
        private  TenantRepository _tenantRepository;
        private  PropertyRepository _propertyRepository;
        private PropertyImageRepository _propertyImageRepository;
        private DealTypeRepository _dealTypeRepository;
        private PropertyTypeRepository _propertyTypeRepository;
        private FavouriteRepository _favouriteRepository;
        private FlagRepository _flagRepository;
        public UnitOfWork(TenantDbContext context, GlobalDbContext globalDbContext)
        {
            _context = context;
            _globalDbContext = globalDbContext;
        }
        public IDealTypeRepository DealTypes => 
            _dealTypeRepository ??= new DealTypeRepository(_context);

        public IFavouriteRepository Favourites => _favouriteRepository ??=new FavouriteRepository(_context);

        public IFlagRepository Flags => _flagRepository ??=new FlagRepository(_context);

        public IPropertyRepository Properties => 
            _propertyRepository ??= new PropertyRepository(_context);
        public IPropertyImageRepository PropertyImages => 
            _propertyImageRepository ??= new PropertyImageRepository(_context);

        public IPropertyTypeRepository PropertyTypes => 
            _propertyTypeRepository ??= new PropertyTypeRepository(_context);

        public ITenantRepository Tenants => _tenantRepository ??= new TenantRepository(_globalDbContext);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
