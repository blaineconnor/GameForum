using Game.Forum.UI.Models.DTOs.Accounts;
using Game.Forum.UI.Models.RequestModels.Accounts;
using Game.Forum.UI.Models.Wrapper;
using Game.Forum.UI.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Game.Forum.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRestService _restService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public AccountController(IRestService restService, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _restService = restService;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM loginModel, [FromQuery] string ReturnUrl)
        {
            //Model doğrulamasını geçemeyen kullanıcıyı buradan tekrar login sayfasına gönder.
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var response = await _restService.PostAsync<LoginVM, Result<TokenDto>>(loginModel, "account/login", false);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", response.Data.Errors[0]);
            }
            else
            {
                var sessionKey = _configuration["Application:SessionKey"];
                _contextAccessor.HttpContext.Session.SetString(sessionKey, JsonConvert.SerializeObject(response.Data.Data));

                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home", new { Area = "Admin" }); ;
            }

            return View(loginModel);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM, [FromQuery] string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var response = await _restService.PostAsync<RegisterVM, Result<bool>>(registerVM, "account/register", false);
            return View(registerVM);
        }
    }
}
