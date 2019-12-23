using AspNetCorePropertyPro.Core;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class DealTypeService : IDealTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DealTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DealType> CreateDealType(DealType dealType)
        {
            await _unitOfWork.DealTypes.AddAsync(dealType);
            await _unitOfWork.CommitAsync();
            return dealType;
        }

        public async Task DeleteDealType(DealType dealType)
        {
            _unitOfWork.DealTypes.RemoveAsync(dealType);
            await _unitOfWork.CommitAsync();
        }

        public async Task<DealType> UpdateDealType(DealType dealTypeToUpdate, DealType dealType)
        {
            dealTypeToUpdate.Id = dealType.Id;
            dealTypeToUpdate.Name = dealType.Name;
            dealTypeToUpdate.Description = dealType.Description;

            await _unitOfWork.CommitAsync();

            return dealType;
        }
    }
}
