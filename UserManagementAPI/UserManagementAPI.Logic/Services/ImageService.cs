using Azure.Storage.Blobs;
using Azure.Storage.Sas;
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
        private BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public ImageService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("AzureStorage"));
            _containerName = configuration["AzureBlobSettings:ImageContainerName"];
        }

        public async Task<Result> DeleteImageAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            
            if (!await containerClient.ExistsAsync())
            {
                return new Result(500, "Container with specified name not exists");
            }

            var blobClient = containerClient.GetBlobClient(name);
            await blobClient.DeleteIfExistsAsync();

            return new Result();
        }

        public async Task<Result<string>> GetImageUrlWithSasTokenAsync(string imageName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            if (!await containerClient.ExistsAsync())
            {
                return new Result<string>(500,"Container with specified name not exists");
            }

            var blobClient = containerClient.GetBlobClient(imageName);
            if (!await blobClient.ExistsAsync())
            {
                return new Result<string>(404, "Image not exists");
            }

            if (blobClient.CanGenerateSasUri)
            {
                var url = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(10));
                return new Result<string>(url.ToString());
            }
            else
            {
                return new Result<string>(500, "Unable to generate uri for this blob");
            }
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

            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            if (!await containerClient.ExistsAsync())
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(_containerName);

                if(!await containerClient.ExistsAsync())
                {
                    return new Result<string>(500, "Unable to create container for images");
                }
            }

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(stream);

            return new Result<string>(fileName);
        }
    }
}
