using AspNetCorePropertyPro.Core;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using AspNetCorePropertyPro.Configuration;

namespace AspNetCorePropertyPro.Services
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckPropertyHasMainImage(int propertyId)
        {
            return await _unitOfWork.PropertyImages.AnyAsync(p => p.PropertyId == propertyId && p.IsMain);
        }

        public async Task<PropertyImage> DeletePropertyImage(PropertyImage propertyImage)
        {
            _unitOfWork.PropertyImages.RemoveAsync(propertyImage);
            await _unitOfWork.CommitAsync();
            return propertyImage;
        }

        public async Task<PropertyImage> GetImageById(int id)
        {
            return await _unitOfWork.PropertyImages.GetById(id);
        }

        public async Task<IEnumerable<PropertyImage>> GetPropertyImages(int propertyId)
        {
            return await _unitOfWork.PropertyImages.Find(p => p.PropertyId == propertyId);
        }

        public async Task<PropertyImage> GetPropertyMainImage(int propertyId)
        {
            return await _unitOfWork.PropertyImages.SingleorDefaultAsync(p => p.PropertyId == propertyId && p.IsMain);
        }

        public async Task<PropertyImage> SavePropertyImage(int propertyId, ImageUploadResult imageUploadResult)
        {
            var propertyImage = new PropertyImage
            {
                PropertyId = propertyId,
                Public_Id = imageUploadResult.PublicId,
                Secure_Url = imageUploadResult.SecureUri.ToString(),
                Url = imageUploadResult.Uri.ToString()
            };

            await _unitOfWork.PropertyImages.AddAsync(propertyImage);
            await _unitOfWork.CommitAsync();
            return propertyImage;
        }

        public async Task<PropertyImage> ToggleMainImageState(PropertyImage propertyImage)
        {
            propertyImage.IsMain = !propertyImage.IsMain;
            await _unitOfWork.CommitAsync();
            return propertyImage;
        }
    }
}
