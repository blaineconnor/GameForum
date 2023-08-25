using AutoMapper;
using Game.Forum.UI.Models.DTOs.Category;
using Game.Forum.UI.Models.RequestModels.Categories;
using Game.Forum.UI.Models.Wrapper;
using Game.Forum.UI.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.Forum.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class CategoryController : Controller
    {
        private IRestService _restService;
        private readonly IMapper _mapper;

        public CategoryController(IRestService restService, IMapper mapper)
        {
            _restService = restService;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            ViewBag.Header = "Kategori İşlemleri";
            ViewBag.Title = "Yeni Kategori Oluştur";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            var response = await _restService.PostAsync<CreateCategoryVM, Result<int>>(categoryModel, "category/create");
          

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", response.Data.Errors[0]);
                return View();
            }
            else
            {
                TempData["success"] = $"{response.Data.Data} numaralı kayıt başarıyla eklendi.";
                return RedirectToAction("List", "Category", new { Area = "Admin" });
            }
        }


        public async Task<IActionResult> List()
        {
            ViewBag.Header = "Kategori İşlemleri";
            ViewBag.Title = "Kategori Düzenle";

            var response = await _restService.GetAsync<Result<List<CategoryDto>>>("category/get");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", "İşlem esnasında sunucu taraflı bir hata oluştu. Lütfen sistem yöneticinize başvurunuz.");
                return View();
            }
            else
            {
                return View(response.Data.Data);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Header = "Kategori İşlemleri";
            ViewBag.Title = "Kategori Güncelle";

            var response = await _restService.GetAsync<Result<CategoryDto>>($"category/get/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", response.Data.Errors[0]);
                return View();
            }
            else 
            {
                var model = _mapper.Map<UpdateCategoryVM>(response.Data.Data);
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryVM updateCategoryModel)
        {
            var response = await _restService.PutAsync<UpdateCategoryVM, Result<int>>(updateCategoryModel, $"category/update/{updateCategoryModel.Id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", response.Data.Errors[0]);
                return View();
            }
            else 
            {
                TempData["success"] = $"{response.Data.Data} numaralı kayıt başarıyla güncellendi.";
                return RedirectToAction("List", "Category", new { Area = "Admin" });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _restService.DeleteAsync<Result<int>>($"category/delete/{id}");

            return Json(response.Data);

        }
    }
}
