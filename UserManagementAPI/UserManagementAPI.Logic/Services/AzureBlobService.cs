using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
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
        public async Task<bool> UploadBlobAsync(Stream stream, string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if(!await containerClient.ExistsAsync())
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
            }

            if (stream.Length > 0)
            {
                var blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(stream);
            }

            return true;
        }
    }
}
