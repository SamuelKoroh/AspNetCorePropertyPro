using AspNetCorePropertyPro.Core;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropertyType> CreatePropertyType(PropertyType propertyType)
        {
            await _unitOfWork.PropertyTypes.AddAsync(propertyType);
            await _unitOfWork.CommitAsync();
            return propertyType;
        }

        public async Task DeletePropertyType(PropertyType propertyType)
        {
            _unitOfWork.PropertyTypes.RemoveAsync(propertyType);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PropertyType> FindPropertyTypeById(int id)
        {
            return await _unitOfWork.PropertyTypes.GetById(id);
        }

        public async Task<PropertyType> FindPropertyTypeByName(string name)
        {
            return await _unitOfWork.PropertyTypes.SingleorDefaultAsync(pt => 
                pt.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<PropertyType>> GetAllPropertTypes()
        {
            return await _unitOfWork.PropertyTypes.GetAllAsync();
        }

        public async Task<PropertyType> UpdatePropertyType(PropertyType propertyTypeToUpdate, PropertyType propertyType)
        {
            propertyTypeToUpdate.Name = propertyType.Name;
            propertyTypeToUpdate.Description = propertyType.Description;

            await _unitOfWork.CommitAsync();

            return propertyTypeToUpdate;
        }
    }
}
