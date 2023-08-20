using Game.Forum.Application.Models.DTOs.Answer;
using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.DTOs.Response;
using Game.Forum.Application.Models.RequestModels.Answers;
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
        public async Task<IActionResult> AddAnswer(AddAnswerVM addAnswerDto)
        {
            await _answerService.AddAnswerAsync(addAnswerDto);
            return Ok(CustomResponseDto.Success(null, HttpStatusCode.OK));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteAnswer(DeleteDto deleteDto)
        {
            await _answerService.DeleteAnswerAsync(deleteDto);
            return Ok(CustomResponseDto.Success("Yanıt Silindi", HttpStatusCode.OK));

        }


    }
}
