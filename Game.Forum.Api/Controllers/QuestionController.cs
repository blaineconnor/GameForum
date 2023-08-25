using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Question;
using Game.Forum.Application.Models.RequestModels.User;
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
        public async Task<IActionResult> AddQuestion(AddQuestionVM addQuestionVM)
        {
            await _questionService.AddQuestionAsync(addQuestionVM);
            return Ok(UserResponseVM.Success(null, HttpStatusCode.OK));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetNewestQuestion(Pagination paginationVM)
        {
            var questions = await _questionService.GetNewestQuestions(paginationVM);
            return Ok(UserResponseVM.Success(questions, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetQuestionsByDescendingVote(Pagination paginationVM)
        {
            var questions = await _questionService.GetQuestionsByDescendingVote(paginationVM);
            return Ok(UserResponseVM.Success(questions, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetMostAnsweredQuestion(Pagination paginationVM)
        {
            var questions = await _questionService.GetQuestionsByDescendingAnswer(paginationVM);
            return Ok(UserResponseVM.Success(questions, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetQuestionDetail(int id, int userId)
        {
            var questionDetails = await _questionService.GetQuestionsWithDetail(id, userId);
            return Ok(UserResponseVM.Success(questionDetails, HttpStatusCode.OK));
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteQuestion(DeleteDto deleteDto)
        {
            await _questionService.DeleteQuestion(deleteDto);
            return Ok(UserResponseVM.Success(null, HttpStatusCode.OK));
        }
    }
}
