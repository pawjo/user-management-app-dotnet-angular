﻿using Microsoft.AspNetCore.Http;
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

        private const string imageRoute = "/image/{userId}";

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

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int userId)
        {
            var result = await _service.DeleteAsync(userId);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _service.GetListAsync();

            return Ok(result.Response);
        }

        [HttpGet("details/{userId}")]
        public async Task<IActionResult> GetDetailsAsync([FromRoute] int userId)
        {
            var result = await _service.GetDetailsAsync(userId);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok(result.Response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
        {
            var result = await _service.UpdateAsync(request);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok();
        }

        [HttpPut(imageRoute)]
        public async Task<IActionResult> UpdateUserImageAsync([FromForm] IFormFile image, [FromRoute] int userId)
        {
            var result = await _service.UpdateUserImageAsync(userId, image);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok();
        }

        [HttpDelete(imageRoute)]
        public async Task<IActionResult> DeleteUserImageAsync([FromRoute] int userId)
        {
            var result = await _service.DeleteUserImageAsync(userId);

            if (result.IsError)
            {
                return StatusCode(result.ErrorCode, result.ErrorMessage);
            }

            return Ok();
        }
    }
}
