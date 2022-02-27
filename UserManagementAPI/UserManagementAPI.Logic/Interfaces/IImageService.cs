using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile image);
    }
}
