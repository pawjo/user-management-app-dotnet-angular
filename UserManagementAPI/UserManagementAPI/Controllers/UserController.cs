using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagementAPI.Logic.Dtos;
using UserManagementAPI.Logic.Interfaces;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddUserRequest request)
        {
            var result = await _service.AddAsync(request);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok(result.Response);
        }

        [HttpPut("/image/{userId}")]
        public async Task<IActionResult> UpdateUserImageAsync([FromForm] IFormFile image, [FromRoute] int userId)
        {
            var result = await _service.UpdateUserImageAsync(userId, image);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok();
        }
    }
}
