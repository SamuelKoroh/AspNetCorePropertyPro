using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IPropertyTypeService
    {
        Task<IEnumerable<PropertyType>> GetAllPropertTypes();
        Task<PropertyType> CreatePropertyType(PropertyType propertyType);
        Task<PropertyType> FindPropertyTypeByName(string name);
        Task<PropertyType> FindPropertyTypeById(int id);
        Task<PropertyType> UpdatePropertyType(PropertyType propertyTypeToUpdate, PropertyType propertyType);
        Task DeletePropertyType(PropertyType propertyType);
    }
}
