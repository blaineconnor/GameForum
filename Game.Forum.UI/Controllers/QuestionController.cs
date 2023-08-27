using AutoMapper;
using Game.Forum.UI.Models.DTOs.Question;
using Game.Forum.UI.Models.RequestModels.Question;
using Game.Forum.UI.Models.Wrapper;
using Game.Forum.UI.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.Forum.UI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IRestService _restService;

        public QuestionController(IRestService restService, IMapper mapper)
        {
            _restService = restService;

        }
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionVM addQuestionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(addQuestionVM);
            }
            var response = await _restService.PostAsync<Result<List<AddQuestionVM>>>("question/details");

            return View(addQuestionVM);
        }

       [HttpGet]
        public async Task<IActionResult> List()
        {
            var response = await _restService.GetAsync<Result<List<GetAllQuestionsVM>>>("category/get");

            return View(response.Data.Data);
        }
    }
}

