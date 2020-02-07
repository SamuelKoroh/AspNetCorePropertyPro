using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadPhoto(IFormFile file);
        Task<DeletionResult> DeletePhoto(string publicId);
    }
}
