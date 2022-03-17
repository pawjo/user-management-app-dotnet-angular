using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Logic.Services
{
    public class UserImageService : IUserImageService
    {
        private readonly IImageService _imageService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public UserImageService(IImageService imageService, IUserService userService, IConfiguration configuration, IMapper mapper)
        {
            _imageService = imageService;
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Result> DeleteUserImageAsync(int userId)
        {
            var imageNameResult = await _userService.GetUserImageNameAsync(userId);

            if (imageNameResult.IsError)
            {
                return imageNameResult;
            }

            var imageName = imageNameResult.Value;
            var defaultImageName = GetDefaultImageName();

            // If user have empty image name it need change to default image name
            var isUpdateNeeded = string.IsNullOrWhiteSpace(imageName);

            // User have image if he doesn't have default or empty image name
            var userHaveImage = imageName != defaultImageName && !isUpdateNeeded;

            if (userHaveImage)
            {
                var deleteImageResult = await _imageService.DeleteImageAsync(imageName);

                if (deleteImageResult.IsError)
                {
                    return new Result(500, "Delete image error");
                }

                isUpdateNeeded = true;
            }

            var result = new Result();

            if (isUpdateNeeded)
            {
                result = await _userService.UpdateImageNameAsync(userId, defaultImageName);
            }

            if (!userHaveImage)
            {
                // error during image name update
                if (result.IsError)
                {
                    result = new Result(500, "Correction wrong user name failed");
                }
                result = new Result(400, "User already doesn't have an image");
            }

            return result;
        }

        public async Task<Result<GetUserImageUrlResponse>> GetUserImageUrlAsync(int userId)
        {
            var imageNameResult = await _userService.GetUserImageNameAsync(userId);

            if (imageNameResult.IsError)
            {
                return new Result<GetUserImageUrlResponse>(imageNameResult);
            }

            var urlResult = await _imageService.GetImageUrlWithSasTokenAsync(imageNameResult.Value);

            if (urlResult.IsError)
            {
                return new Result<GetUserImageUrlResponse>(urlResult);
            }

            var urlData = urlResult as Result<Tuple<string, DateTimeOffset>>;
            var result = _mapper.Map<GetUserImageUrlResponse>(urlData.Value, opt =>
                opt.Items["userId"] = userId);

            return new Result<GetUserImageUrlResponse>(result);
        }

        public async Task<Result> UpdateUserImageAsync(int userId, IFormFile image)
        {
            var imageNameResult = await _userService.GetUserImageNameAsync(userId);

            if (imageNameResult.IsError)
            {
                return imageNameResult;
            }

            var imageName = imageNameResult.Value;
            if (imageName != GetDefaultImageName())
            {
                var deleteImageResult = await _imageService.DeleteImageAsync(imageName);

                if (deleteImageResult.IsError)
                {
                    return new Result(500, "Delete image error");
                }
            }

            var imageUploadResult = await _imageService.UploadImageAsync(image);

            if (imageUploadResult.IsError)
            {
                return imageUploadResult;
            }

            var updateUserResult = await _userService.UpdateImageNameAsync(userId, imageUploadResult.Value);

            if (updateUserResult.IsError)
            {
                return updateUserResult;
            }

            return new Result();
        }

        private string GetDefaultImageName() =>
            _configuration["AzureBlobSettings:DefaultUserImageName"];

        private string GetUrlExpirationTime() =>
            _configuration["AzureBlobSettings:SasTokenExpirationTimeInMinutes"];
    }
}
