using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
        private readonly IConfiguration _configuration;

        public UserService(DatabaseContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Result<int>> AddAsync(AddUserRequest request)
        {
            var user = _mapper.Map<User>(request, opt =>
                opt.Items["defaultImageName"] = GetDefaultImageName());

            await _context.Users.AddAsync(user);
            var added = await _context.SaveChangesAsync();

            if (added == 1)
            {
                return new Result<int>(user.Id);
            }

            return new Result<int>(500, "Adding user data error");
        }

        public async Task<Result<IEnumerable<UserListItemDto>>> GetListAsync()
        {
            var users = await _context.Users.ToListAsync();

            var response = _mapper.Map<IEnumerable<UserListItemDto>>(users);

            return new Result<IEnumerable<UserListItemDto>>(response);
        }

        public async Task<Result<GetUserByIdResponse>> GetUserById(int userId)
        {
            var user = await GetSingleUserById(userId);

            if (user == null)
            {
                return new Result<GetUserByIdResponse>(404, "User not found");
            }

            var response = _mapper.Map<GetUserByIdResponse>(user);
            return new Result<GetUserByIdResponse>(response);
        }

        public async Task<Result<string>> GetUserImageNameAsync(int userId)
        {
            var user = await GetSingleUserById(userId);

            if (user == null)
            {
                return new Result<string>(404, "User not found");
            }

            return new Result<string>(user.ImageName);
        }

        public async Task<Result> UpdateImageNameAsync(int userId, string newImageName)
        {
            var user = await GetSingleUserById(userId);

            if (user == null)
            {
                return new Result(404, "User not found");
            }

            user.ImageName = newImageName;
            var updated = await _context.SaveChangesAsync();
            if (updated != 1)
            {
                return new Result(500, "Update user image name failed");
            }

            return new Result();
        }

        private string GetDefaultImageName() =>
            _configuration["AzureBlobSettings:DefaultUserImageName"];

        private async Task<User> GetSingleUserById(int userId) =>
            await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
    }
}
