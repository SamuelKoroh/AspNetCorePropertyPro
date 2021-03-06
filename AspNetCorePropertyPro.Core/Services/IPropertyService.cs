﻿using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IPropertyService 
    {
        Task<IEnumerable<Property>> GetAllProperty();
        Task<IEnumerable<Property>> GetPropertiesByOwnerId(string ownerId);
        Task<Property> GetPropertById(int id);
        Task<bool> CheckIfPropertyTitleExists(string title);
        Task<Property> CreateProperty(Property newProperty, string  userId);
        Task<Property> UpdateProperty(Property propertyToUpdate, Property property);
        Task DeleteProperty(Property property);
    }
}
