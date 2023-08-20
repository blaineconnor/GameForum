using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.DTOs.Response;
using Game.Forum.Application.Models.RequestModels.Users;
using Game.Forum.Application.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.Forum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUser(RegisterVM user)
        {
            await _service.Register(user);
            return Ok(CustomResponseDto.Success(null, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginVM login)
        {
            await _service.Login(login);
            return Ok(CustomResponseDto.Success(null, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUser(DeleteDto delete)
        {
            await _service.DeleteUser(delete);
            return Ok(CustomResponseDto.Success(null, HttpStatusCode.OK));
        }

    }
}
