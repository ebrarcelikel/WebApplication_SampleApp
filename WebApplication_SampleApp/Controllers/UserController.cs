using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication_SampleApp.Bussines;
using WebApplication_SampleApp.Models;
using WebApplication_SampleApp.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication_SampleApp.Controllers
{
    public class UserController : Controller

    {
        private readonly UserService _userService;
        public UserController()
        {
            _userService = new UserService(HttpContext);

        }

        public IActionResult Index()
        {
            ServiceResult<List<User>> result = _userService.List();
            return View(result.Data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                ServiceResult<User> result = _userService.Create(model, HttpContext);
                if (!result.IsError)
                    return RedirectToAction(nameof(Index));
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }


            }
            return View(model);
        }
        public IActionResult Edit(int ID)
        {
            ServiceResult<User> result = _userService.Find(ID);
            if (result.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            UserEditViewModel model = new UserEditViewModel
            {
                Fullname = result.Data.Fullname,
                Username = result.Data.Username,
                Email = result.Data.Email,
                IsActive = result.Data.IsActive,
                IsAdmin = result.Data.IsAdmin,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int ID, UserEditViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                ServiceResult<User> result = _userService.Update(ID, model, HttpContext);
                if (!result.IsError)
                    return RedirectToAction(nameof(Index));
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }


            }
            return View(model);

        }
        public IActionResult Details(int ID)
        {
            ServiceResult<User> result = _userService.Find(ID);
            if (result.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }
        public IActionResult Delete(int ID)
        {
            ServiceResult<User> result = _userService.Find(ID);
            if (result.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int ID)
        {
            ServiceResult<object> result = _userService.Remove(ID);
            if (!result.IsError)
                return RedirectToAction(nameof(Index));
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return View(_userService.Find(ID).Data);
        }

    }
}
