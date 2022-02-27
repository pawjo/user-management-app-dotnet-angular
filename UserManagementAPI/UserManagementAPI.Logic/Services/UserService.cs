using AutoMapper;
using System.Threading.Tasks;
using UserManagementAPI.Infrastructure;
using UserManagementAPI.Infrastructure.Models;
using UserManagementAPI.Logic.Dtos;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<int>> AddAsync(AddUserRequest request)
        {
            var user = _mapper.Map<User>(request);

            await _context.Users.AddAsync(user);
            var added = await _context.SaveChangesAsync();

            if (added == 1)
            {
                return new Result<int>(user.Id);
            }

            return new Result<int>(500, "Adding user data error.");
        }
    }
}
