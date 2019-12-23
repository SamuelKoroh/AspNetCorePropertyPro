using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IDealTypeService
    {
        Task<DealType> CreateDealType(DealType dealType);
        Task<DealType> UpdateDealType(DealType dealTypeToUpdate, DealType dealType);
        Task DeleteDealType(DealType dealType);
        Task<DealType> FindDealTypeByName(string name);
        Task<DealType> FindDealTypeById(int id);
        Task<IEnumerable<DealType>> GetAllDealType();

    }
}
