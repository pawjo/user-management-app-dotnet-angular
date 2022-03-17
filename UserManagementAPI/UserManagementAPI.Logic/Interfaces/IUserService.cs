using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IUserService
    {
        Task<Result<int>> AddAsync(AddUserRequest request);

        Task<Result<IEnumerable<UserListItemDto>>> GetListAsync();
        
        Task<Result<GetUserByIdResponse>> GetUserById(int userId);

        Task<Result<string>> GetUserImageNameAsync(int userId);

        Task<Result> UpdateImageNameAsync(int userId, string newImageName);
    }
}
