using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IUserImageService _userImageService;

        public ImageController(IImageService imageService, IUserImageService userImageService)
        {
            _imageService = imageService;
            _userImageService = userImageService;
        }

        [HttpGet("/user-image-url/{userId}")]
        public async Task<IActionResult> GetUserImageUrlAsync([FromRoute]int userId)
        {
            var result = await _userImageService.GetUserImageAsync(userId);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok(result.Response);
        }
    }
}
