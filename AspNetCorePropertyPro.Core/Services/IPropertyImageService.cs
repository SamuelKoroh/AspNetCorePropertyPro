using AspNetCorePropertyPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IPropertyImageService
    {
        Task<bool> CheckPropertyHasMainImage(int propertyId);
        Task<PropertyImage> GetImageById(int id);
        Task<PropertyImage> GetPropertyMainImage(int propertyId);
        Task<IEnumerable<PropertyImage>> GetPropertyImages(int propertyId);
        Task<PropertyImage> ToggleMainImageState(PropertyImage propertyImage);
        Task<PropertyImage> SavePropertyImage(int propertyId, ImageUploadResult imageUploadResult);
        Task<PropertyImage> DeletePropertyImage(PropertyImage propertyImage);
    }
}
