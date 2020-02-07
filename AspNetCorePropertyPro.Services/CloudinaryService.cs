using AspNetCorePropertyPro.Configuration;
using AspNetCorePropertyPro.Core.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinarySetting _cloudinarySettings;
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySetting> cloudinarySettings)
        {
            _cloudinarySettings = cloudinarySettings.Value;
            _cloudinary = new Cloudinary(CloudinaryAccount());
        }
        public async Task<DeletionResult> DeletePhoto(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deletionParams);
        }

        public async Task<ImageUploadResult> UploadPhoto(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var ImageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream)
            };
            return await _cloudinary.UploadAsync(ImageUploadParams);
        }

        private Account CloudinaryAccount()
        {
            return new Account 
            { 
                ApiKey = _cloudinarySettings.ApiKey,
                ApiSecret = _cloudinarySettings.ApiSecret,
                Cloud = _cloudinarySettings.CloudName
            };
        }
    }
}
