using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IFlagService
    {
        Task<Flag> GetFlag(int id);
        Task<Flag> GetFlagWithProperty(int id);
        Task<IEnumerable<Flag>> GetFlags();
        Task<Flag> CreateFlag(Flag flag);
        void DeleteFlag(Flag flag);
    }
}
