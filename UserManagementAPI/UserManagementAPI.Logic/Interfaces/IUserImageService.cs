using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IUserImageService
    {
        Task<Result> DeleteUserImageAsync(int userId);

        Task<Result<string>> GetUserImageUrlAsync(int userId);

        Task<Result> UpdateUserImageAsync(int userId, IFormFile image);
    }
}
