using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IPropertyImageService
    {
        Task<PropertyImage> CreatePropertyImage(PropertyImage propertyImage);
        Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImageToUpdate, PropertyImage propertyImage);
        Task<PropertyImage> DeletePropertyImage(PropertyImage propertyImage);
    }
}
