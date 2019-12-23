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
        private readonly IOptions<CloudinarySetting> _cloudinarySetting;

        public PropertyImageService(IUnitOfWork unitOfWork, IOptions<CloudinarySetting> cloudinarySetting)
        {
            _unitOfWork = unitOfWork;
            _cloudinarySetting = cloudinarySetting;
        }
        public Task<PropertyImage> CreatePropertyImage(PropertyImage propertyImage)
        {
            var account = new Account(_cloudinarySetting.Value.CloudName, 
                _cloudinarySetting.Value.ApiKey, _cloudinarySetting.Value.ApiSecret);

            var cloudinary = new Cloudinary(account);

            throw new NotImplementedException();
        }

        public Task<PropertyImage> DeletePropertyImage(PropertyImage propertyImage)
        {
            throw new NotImplementedException();
        }

        public Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImageToUpdate, PropertyImage propertyImage)
        {
            throw new NotImplementedException();
        }
    }
}
