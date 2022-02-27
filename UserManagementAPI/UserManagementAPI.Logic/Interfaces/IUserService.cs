using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IUserService
    {
        Task<Result<int>> AddAsync(AddUserRequest request);

        Task<Result> DeleteUserImageAsync(int userId);

        Task<Result<IEnumerable<UserListItemDto>>> GetListAsync();

        Task<Result> UpdateUserImageAsync(int userId, IFormFile image);
    }
}
