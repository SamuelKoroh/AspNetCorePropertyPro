using AspNetCorePropertyPro.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IDealTypeRepository DealTypes { get; }
        ITenantRepository Tenants { get; }
        IFavouriteRepository  Favourites { get; }
        IFlagRepository Flags { get; }
        IPropertyRepository  Properties { get; }
        IPropertyTypeRepository PropertyTypes { get; }
        IPropertyImageRepository PropertyImages { get; }
        Task<int> CommitAsync();
    }
}
