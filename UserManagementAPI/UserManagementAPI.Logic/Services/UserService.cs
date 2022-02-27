using System.Threading.Tasks;
using UserManagementAPI.Infrastructure;
using UserManagementAPI.Logic.Dtos;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> AddAsync(AddUserRequest request)
        {
            return new Result<int>(1);
        }
    }
}
