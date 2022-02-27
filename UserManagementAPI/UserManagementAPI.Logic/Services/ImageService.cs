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
        private readonly IAzureBlobService _blobService;
        private readonly string _containerName;

        public ImageService(IAzureBlobService blobService, IConfiguration configuration)
        {
            _blobService = blobService;
            _containerName = configuration["AzureBlobSettings:ImageContainerName"];
        }

        public async Task<Result> DeleteImageAsync(string name)
        {
            try
            {
                await _blobService.DeleteBlobAsync(name, _containerName);
            }
            catch (Exception e)
            {
                return new Result(500, e.Message);
            }

            return new Result();
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

            try
            {
                await _blobService.UploadBlobAsync(stream, fileName, _containerName);
            }
            catch (Exception e)
            {
                return new Result<string>(500, e.Message);
            }

            return new Result<string>(fileName);
        }
    }
}
