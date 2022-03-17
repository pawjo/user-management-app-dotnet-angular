using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IUserImageService _userImageService;
        private const string userIdParam = "/{userId}";

        public ImageController(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }

        [HttpDelete(userIdParam)]
        public async Task<IActionResult> DeleteUserImageAsync([FromRoute] int userId)
        {
            var result = await _userImageService.DeleteUserImageAsync(userId);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet("/user-image-url" + userIdParam)]
        public async Task<IActionResult> GetUserImageUrlAsync([FromRoute] int userId)
        {
            var result = await _userImageService.GetUserImageUrlAsync(userId);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok(result.Response);
        }

        [HttpPut(userIdParam)]
        public async Task<IActionResult> UpdateUserImageAsync([FromForm] IFormFile image, [FromRoute] int userId)
        {
            var result = await _userImageService.UpdateUserImageAsync(userId, image);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok();
        }
    }
}
