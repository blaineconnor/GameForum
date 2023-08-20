using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.DTOs.Response;
using Game.Forum.Application.Models.RequestModels.Question;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.Forum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddQuestion(AddQuestionVM addQuestion)
        {
            await _questionService.AddQuestionAsync(addQuestion);
            return Ok(CustomResponseDto.Success(null, HttpStatusCode.OK)); // null yerine addQuestionContract yazılabilir diye düşündük
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetNewestQuestion(Pagination pagination)
        {
            var questions = await _questionService.GetNewestQuestions(pagination);
            return Ok(CustomResponseDto.Success(questions, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetQuestionsByDescendingVote(Pagination pagination)
        {
            var questions = await _questionService.GetQuestionsByDescendingVote(pagination);
            return Ok(CustomResponseDto.Success(questions, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetMostAnsweredQuestion(Pagination pagination)
        {
            var questions = await _questionService.GetQuestionsByDescendingAnswer(pagination);
            return Ok(CustomResponseDto.Success(questions, HttpStatusCode.OK));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddQuestionToFav(AddQuestionToFavVM addQuestionToFav)
        {

            await _questionService.AddQuestionToFavAsync(addQuestionToFav);
            return Ok(CustomResponseDto.Success(null, HttpStatusCode.OK));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuestionDetail(int id, int userId)
        {
            var questionDetails = await _questionService.GetQuestionsWithDetail(id, userId);
            return Ok(CustomResponseDto.Success(questionDetails, HttpStatusCode.OK));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteQuestion(DeleteDto deleteContract)
        {
            await _questionService.DeleteQuestion(deleteContract);
            return Ok(CustomResponseDto.Success(null, HttpStatusCode.OK));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteFavorite(DeleteDto deleteContract)
        {
            await _questionService.DeleteFavorite(deleteContract);
            return Ok(CustomResponseDto.Success(null, HttpStatusCode.OK));
        }



    }
}
