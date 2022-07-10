using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Logic.Services
{

    public class AzureBlobService : IAzureBlobService
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobService(IConfiguration configuration)
        {
            _configuration = configuration;
            _blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureStorage"));
        }

        public async Task DeleteBlobAsync(string name, string containerName)
        {
            var blobClient = await GetExistingBlobClient(name, containerName);
            await blobClient.DeleteIfExistsAsync();
        }



        public async Task<Result<SasUrlDto>> GetServiceSasUriForBlob(string name, string containerName)
        {
            var blobClient = await GetExistingBlobClient(name, containerName);

            if (!await blobClient.ExistsAsync())
            {
                return new Result<SasUrlDto>(404, "Not found file");
            }

            if (blobClient.CanGenerateSasUri)
            {
                var expirationTime = int.Parse(_configuration["AzureBlobSettings:SasExpirationTimeInMinutes"]);

                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = name,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(expirationTime)
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                var sasUri = blobClient.GenerateSasUri(sasBuilder);
                var result = new SasUrlDto
                {
                    Name = name,
                    Url = sasUri.ToString(),
                    ExpiresOn = sasBuilder.ExpiresOn.DateTime
                };

                return new Result<SasUrlDto>(result);
            }

            return new Result<SasUrlDto>(400, "Cannot generate sas uri");
        }

        public async Task UploadBlobAsync(Stream stream, string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!await containerClient.ExistsAsync())
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
            }

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(stream, true);
        }

        private async Task<BlobClient> GetExistingBlobClient(string name, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!await containerClient.ExistsAsync())
            {
                throw new Exception("Container with specified name not exists");
            }
            var blobClient = containerClient.GetBlobClient(name);
            return blobClient;
        }
    }
}
