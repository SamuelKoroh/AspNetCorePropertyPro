using AspNetCorePropertyPro.Core;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Property> CreateProperty(Property newProperty)
        {
            await _unitOfWork.Properties.AddAsync(newProperty);
            await _unitOfWork.CommitAsync();
            return newProperty;
        }

        public async Task DeleteProperty(Property property)
        {
            _unitOfWork.Properties.RemoveAsync(property);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Property>> GetAllProperty()
        {
            return await _unitOfWork.Properties.GetAllAsync();
        }

        public async Task<Property> GetPropertById(int id)
        {
            return await _unitOfWork.Properties.GetWithOwnerAsync(id);
        }

        public async Task<IEnumerable<Property>> GetPropertiesByOwnerId(string ownerId)
        {
            return await _unitOfWork.Properties.GetAllWithOwnerByOwnerIdAsync(ownerId);
        }

        public async Task<Property> UpdateProperty(Property propertyToUpdate, Property property)
        {
            propertyToUpdate.Title = property.Title;
            propertyToUpdate.Price = property.Price;
            propertyToUpdate.City = property.City;
            propertyToUpdate.State = property.State;
            propertyToUpdate.Address = property.Address;
            propertyToUpdate.IsActive = property.IsActive;
            propertyToUpdate.Status = property.Status;
            propertyToUpdate.Description = property.Description;

            await _unitOfWork.CommitAsync();

            return propertyToUpdate;
        }
    }
}
