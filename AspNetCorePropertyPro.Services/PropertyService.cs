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

        public async Task<bool> CheckIfPropertyTitleExists(string title)
        {
            return await _unitOfWork.Properties.AnyAsync(x=>x.Title.Trim().ToLower() == title.Trim().ToLower());
        }

        public async Task<Property> CreateProperty(Property newProperty, string userId)
        {
            newProperty.OwnerId = userId;
            newProperty.DateAdded = DateTime.Now;
            newProperty.LastUpdatedDate = DateTime.Now;

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
            return await _unitOfWork.Properties.GetAllWithUserAsync();
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
            propertyToUpdate.LastUpdatedDate = DateTime.Now;

            await _unitOfWork.CommitAsync();

            return propertyToUpdate;
        }
    }
}
