using System.IO;
using System.Threading.Tasks;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IAzureBlobService
    {
        Task<bool> UploadBlobAsync(Stream stream, string fileName, string containerName);
    }
}
