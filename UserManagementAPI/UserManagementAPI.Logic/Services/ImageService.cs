using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;
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

        public async Task<Result<string>> UploadImageAsync(IFormFile image)
        {
            if (image.Length == 0)
            {
                return new Result<string>(400, "File is empty");
            }

            var stream = image.OpenReadStream();
            var extension = Path.GetExtension(image.FileName);
            var fileName = Guid.NewGuid() + extension;
            var containerName = _configuration["AzureBlobSettings:ImageContainerName"];

            try
            {
                await _blobService.UploadBlobAsync(stream, fileName, containerName);
            }
            catch (Exception e)
            {
                return new Result<string>(500, e.Message);
            }

            return new Result<string>(fileName);
        }
    }
}
