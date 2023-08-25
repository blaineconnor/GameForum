using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Answers;
using Game.Forum.Application.Models.RequestModels.User;
using Game.Forum.Application.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.Forum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAnswer(AddAnswerVM addAnswerVM)
        {
            await _answerService.AddAnswerAsync(addAnswerVM);
            return Ok(UserResponseVM.Success(null, HttpStatusCode.OK));
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAnswer(DeleteAnswerVM deleteAnswerVM)
        {
            await _answerService.DeleteAnswerAsync(deleteAnswerVM);
            return Ok(UserResponseVM.Success("Yanıt Silindi", HttpStatusCode.OK));

        }
    }
}
