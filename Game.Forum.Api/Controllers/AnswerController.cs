using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Answers;
using Game.Forum.Application.Models.RequestModels.Custom;
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
            return Ok(ResponseVM.Success(null, HttpStatusCode.OK));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteAnswer(DeleteDto deleteDto)
        {
            await _answerService.DeleteAnswerAsync(deleteDto);
            return Ok(ResponseVM.Success("Yanıt Silindi", HttpStatusCode.OK));

        }


    }
}
