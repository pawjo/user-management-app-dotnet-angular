using System.IO;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IAzureBlobService
    {
        Task UploadBlobAsync(Stream stream, string fileName, string containerName);

        Task DeleteBlobAsync(string name, string containerName);

        Task<Result<SasUrlDto>> GetServiceSasUriForBlob(string name, string containerName);
    }
}
