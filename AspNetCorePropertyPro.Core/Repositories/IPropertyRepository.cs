using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Repositories
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<IEnumerable<Property>> GetAllWithUserAsync();
        Task<IEnumerable<Property>> GetAllWithOwnerByOwnerIdAsync(string ownerId);
        Task<Property> GetWithOwnerAsync(int id);
    }
}