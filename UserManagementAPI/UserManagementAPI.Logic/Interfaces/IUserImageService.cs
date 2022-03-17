using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.Interfaces
{
    public interface IUserImageService
    {
        Task<Result<string>> GetUserImageAsync(int userId);
    }
}
