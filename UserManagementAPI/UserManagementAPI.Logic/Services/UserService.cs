using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UserManagementAPI.Infrastructure;
using UserManagementAPI.Logic.Dtos;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IImageService _imageService;

        public UserService(DatabaseContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<Result<int>> AddAsync(AddUserRequest request, IFormFile image)
        {
            var name = await _imageService.UploadImageAsync(image);
            return new Result<int>(1);
        }
    }
}
