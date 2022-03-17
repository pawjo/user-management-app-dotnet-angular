using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Logic.Services
{

    public class AzureBlobService : IAzureBlobService
    {
        private BlobServiceClient _blobServiceClient;

        public AzureBlobService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("AzureStorage"));
        }

        public async Task DeleteBlobAsync(string name, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!await containerClient.ExistsAsync())
            {
                throw new Exception("Container with specified name not exists");
            }

            var blobClient = containerClient.GetBlobClient(name);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> GetSasUrlAsync(string name, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!await containerClient.ExistsAsync())
            {
                throw new Exception("Container with specified name not exists");
            }

            var blobClient = containerClient.GetBlobClient(name);
            if (blobClient.CanGenerateSasUri)
            {
                var builder = new BlobSasBuilder(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(10));
                var url = blobClient.GenerateSasUri(builder);
                return url.ToString();
            }
            else
            {
                throw new Exception("Unable to generate uri for this blob");
            }
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
    }
}
