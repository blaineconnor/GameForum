using AutoMapper;
using Game.Forum.UI.Models.DTOs.Category;
using Game.Forum.UI.Models.Wrapper;
using Game.Forum.UI.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Game.Forum.UI.Controllers
{
    public class CategoryController : Controller
    {
        private IRestService _restService;
        private readonly IMapper _mapper;

        public CategoryController(IRestService restService, IMapper mapper)
        {
            _restService = restService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            ViewBag.Header = "Kategori İşlemleri";
            ViewBag.Title = "Kategori Düzenle";

            var response = await _restService.GetAsync<Result<List<CategoryDto>>>("category/get");

            return View(response.Data.Data);
        }

    }
}
