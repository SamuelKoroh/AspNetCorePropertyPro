using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
