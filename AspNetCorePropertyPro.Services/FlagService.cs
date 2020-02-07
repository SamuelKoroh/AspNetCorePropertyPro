using AspNetCorePropertyPro.Core;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class FlagService : IFlagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Flag> CreateFlag(Flag flag)
        {
            await _unitOfWork.Flags.AddAsync(flag);
            await _unitOfWork.CommitAsync();

            return flag;
        }

        public void DeleteFlag(Flag flag)
        {
            _unitOfWork.Flags.RemoveAsync(flag);
            _unitOfWork.CommitAsync();
        }

        public async Task<Flag> GetFlag(int id)
        {
            return await _unitOfWork.Flags.GetById(id);
        }

        public async Task<IEnumerable<Flag>> GetFlags()
        {
            return await _unitOfWork.Flags.GetAllFlagsWithProperty();
        }

        public async Task<Flag> GetFlagWithProperty(int id)
        {
            return await _unitOfWork.Flags.GetFlagWithPropertyById(id);
        }
    }
}
