using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Logic.Services
{
    public class UserImageService : IUserImageService
    {
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public UserImageService(IImageService imageService, IUserService userService)
        {
            _imageService = imageService;
            _userService = userService;
        }

        public async Task<Result<string>> GetUserImageAsync(int userId)
        {
            var imageNameResult = await _userService.GetUserImageNameAsync(userId);

            if (imageNameResult.IsError)
            {
                return imageNameResult;
            }

            var urlResult = await _imageService.GetImageUrlWithSasTokenAsync(imageNameResult.Response);

            return urlResult;
        }
    }
}
