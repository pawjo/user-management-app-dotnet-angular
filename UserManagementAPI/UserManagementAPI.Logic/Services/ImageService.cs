using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IAzureBlobService _blobService;

        public ImageService(IConfiguration configuration, IAzureBlobService blobService)
        {
            _configuration = configuration;
            _blobService = blobService;
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            var stream = image.OpenReadStream();
            
            var extension = Path.GetExtension(image.FileName);
            var fileName = Guid.NewGuid() + extension;

            //var containerName = _configuration.GetSection("AzureBlobSettings:ImageContainerName").ToString();
            var containerName = _configuration["AzureBlobSettings:ImageContainerName"];

            var result = await _blobService.UploadBlobAsync(stream, fileName, containerName);

            return fileName;
        }
    }
}
