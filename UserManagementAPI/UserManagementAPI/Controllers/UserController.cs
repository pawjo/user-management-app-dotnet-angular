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
        private const string userIdParam = "/{userId}";

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

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _service.GetListAsync();

            return Ok(result.Value);
        }

        [HttpGet("details" + userIdParam)]
        public async Task<IActionResult> GetById([FromRoute]int userId)
        {
            var result = await _service.GetUserById(userId);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok(result.Value);
        }
    }
}
