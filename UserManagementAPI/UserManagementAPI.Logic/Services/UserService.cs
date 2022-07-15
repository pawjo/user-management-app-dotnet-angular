using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;

        public UserService(DatabaseContext context, IMapper mapper, IImageService imageService, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
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

        public async Task<Result> DeleteUserImageAsync(int userId)
        {
            var user = await GetSingleUserById(userId);

            if (user == null)
            {
                return new Result(404, "User not found");
            }

            var defaultImageName = GetDefaultImageName();

            // If user have empty image name it need change to default image name
            var isUpdateNeeded = string.IsNullOrWhiteSpace(user.ImageName);

            // User have image if he doesn't have default or empty image name
            var userHaveImage = user.ImageName != defaultImageName && !isUpdateNeeded;

            if (userHaveImage)
            {
                var deleteImageResult = await _imageService.DeleteImageAsync(user.ImageName);

                if (deleteImageResult.IsError)
                {
                    return new Result(500, "Delete image error");
                }

                isUpdateNeeded = true;
            }

            if (isUpdateNeeded)
            {
                var updated = await UpdateAndSaveUserImageName(user, defaultImageName);
                if (updated != 1)
                {
                    return new Result(500, "User image name update failed");
                }
            }

            if (!userHaveImage)
            {
                return new Result(400, "User already doesn't have an image");
            }

            return new Result();
        }

        public async Task<Result<UserDetailsDto>> GetDetailsAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return new Result<UserDetailsDto>(404, "User not found");
            }

            SasUrlDto image = null;

            if (!string.IsNullOrWhiteSpace(user.ImageName))
            {
                var imageResult = await _imageService.GetImageUrlAsync(user.ImageName);
                if (imageResult.IsError)
                {
                    return new Result<UserDetailsDto>(imageResult.ErrorCode, imageResult.ErrorMessage);
                }
                image = imageResult.Response;
            }

            var response = _mapper.Map<UserDetailsDto>(user, opt =>
                opt.Items["userImage"] = image);

            return new Result<UserDetailsDto>(response);
        }

        public async Task<Result<IEnumerable<UserListItemDto>>> GetListAsync()
        {
            var users = await _context.Users.ToListAsync();

            var response = _mapper.Map<IEnumerable<UserListItemDto>>(users);

            return new Result<IEnumerable<UserListItemDto>>(response);
        }

        public async Task<Result> UpdateAsync(UpdateUserRequest request)
        {
            var user = await GetSingleUserById(request.Id);

            if (user == null)
            {
                return new Result(404, "User not found");
            }

            user = _mapper.Map<UpdateUserRequest, User>(request, user);
            var updated = await _context.SaveChangesAsync();
            if (updated != 1)
            {
                return new Result(500, "User update error");
            }
            return new Result();
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

            var updated = await UpdateAndSaveUserImageName(user, imageUploadResult.Response);

            if (updated != 1)
            {
                return new Result(500, "User update error");
            }

            return new Result();
        }

        private string GetDefaultImageName() =>
            _configuration["AzureBlobSettings:DefaultUserImageName"];

        private async Task<User> GetSingleUserById(int userId) =>
            await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);

        private async Task<int> UpdateAndSaveUserImageName(User user, string newImageName)
        {
            user.ImageName = newImageName;
            return await _context.SaveChangesAsync();
        }
    }
}
