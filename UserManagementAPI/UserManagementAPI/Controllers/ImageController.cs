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

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("sas/{name}")]
        public async Task<IActionResult> GetSasUrlAsync([FromRoute] string name)
        {
            var result = await _imageService.GetImageUrlAsync(name);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok(result.Response);
        }
    }
}
