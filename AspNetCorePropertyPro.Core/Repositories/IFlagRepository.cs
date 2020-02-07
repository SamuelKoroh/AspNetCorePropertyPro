using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Repositories
{
    public interface IFlagRepository : IRepository<Flag>
    {
        Task<Flag> GetFlagWithPropertyById(int id);
        Task<IEnumerable<Flag>> GetAllFlagsWithProperty();
    }
}
