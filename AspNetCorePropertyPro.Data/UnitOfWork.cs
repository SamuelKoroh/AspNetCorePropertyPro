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
        private  PropertyRepository _propertyRepository;
        private PropertyImageRepository _propertyImageRepository;
        private DealTypeRepository _dealTypeRepository;
        private PropertyTypeRepository _propertyTypeRepository;
        public UnitOfWork(TenantDbContext context)
        {
            _context = context;
        }
        public IDealTypeRepository DealTypes => 
            _dealTypeRepository ??= new DealTypeRepository(_context);

        public IFavouriteRepository Favourites => throw new NotImplementedException();

        public IFlagRepository Flags => throw new NotImplementedException();

        public IPropertyRepository Properties => 
            _propertyRepository ??= new PropertyRepository(_context);
        public IPropertyImageRepository PropertyImages => 
            _propertyImageRepository ??= new PropertyImageRepository(_context);

        public IPropertyTypeRepository PropertyTypes => 
            _propertyTypeRepository ??= new PropertyTypeRepository(_context);


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
