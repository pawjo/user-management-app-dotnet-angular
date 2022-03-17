using System.IO;
using System.Threading.Tasks;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IAzureBlobService
    {
        Task UploadBlobAsync(Stream stream, string fileName, string containerName);

        Task DeleteBlobAsync(string name, string containerName);

        Task<string> GetSasUrlAsync(string name, string containerName);
    }
}
