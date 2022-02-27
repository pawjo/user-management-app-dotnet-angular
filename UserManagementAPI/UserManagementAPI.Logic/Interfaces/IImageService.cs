using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IImageService
    {
        Task<Result<string>> UploadImageAsync(IFormFile image);
    }
}
