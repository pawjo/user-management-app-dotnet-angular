using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private readonly IImageService _imageService;

        public UserService(DatabaseContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
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

        public async Task<Result> UpdateUserImageAsync(int userId, IFormFile image)
        {
            var user = await GetSingleUserById(userId);

            if (user == null)
            {
                return new Result(404, "User not found");
            }

            var imageUploadResult = await _imageService.UploadImageAsync(image);

            if (imageUploadResult.IsError)
            {
                return imageUploadResult;
            }

            user.ImageName = imageUploadResult.Response;
            var updated = await _context.SaveChangesAsync();

            if (updated != 1)
            {
                return new Result(500, "User update error");
            }

            return new Result();
        }

        private async Task<User> GetSingleUserById(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }
    }
}
