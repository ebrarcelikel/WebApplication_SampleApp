using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication_SampleApp.Bussines;
using WebApplication_SampleApp.Core;
using WebApplication_SampleApp.Models;
using WebApplication_SampleApp.Models.Entities;

namespace WebApplication_SampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;
        private readonly EBulletinService _eBulletinService;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _userService = new UserService(HttpContext);
            _eBulletinService = new EBulletinService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                ServiceResult<User> result = _userService.Login(model);
                if (!result.IsError)
                {
                    HttpContext.Session.SetInt32(Constants.UserId, result.Data.ID);
                    HttpContext.Session.SetString(Constants.Username, result.Data.Username);
                    HttpContext.Session.SetString(Constants.UserEmail, result.Data.Email);
                    HttpContext.Session.SetString(Constants.UserRole, result.Data.IsAdmin ? "admin" : "member");

                    return RedirectToAction(nameof(Index));
                }
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }


            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                ServiceResult<object> result = _userService.Register(model);
                if (!result.IsError)
                    return RedirectToAction(nameof(Login));
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }


            }
            return View(model);
        }
        [HttpPost]
        public IActionResult SaveEbulletin(string email)
        {
            ServiceResult<object> result = _eBulletinService.Create(email);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ShowProfile()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult DeleteProfile()
        {
            return View();
        }
        public IActionResult LogOut()

        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}