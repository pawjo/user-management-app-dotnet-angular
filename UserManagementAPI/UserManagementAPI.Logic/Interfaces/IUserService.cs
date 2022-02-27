using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IUserService
    {
        Task<Result<int>> AddAsync(AddUserRequest request);

        Task<Result> UpdateUserImageAsync(int userId, IFormFile image);

        Task<Result> DeleteUserImageAsync(int userId);
    }
}
