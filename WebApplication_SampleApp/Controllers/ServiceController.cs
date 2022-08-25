using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebApplication_SampleApp.Bussines;
using WebApplication_SampleApp.Core;
using WebApplication_SampleApp.Models;
using WebApplication_SampleApp.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication_SampleApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly ServiceService _serviceService;

        public ServiceController()
        {
            _categoryService = new CategoryService();
            _serviceService = new ServiceService(HttpContext);


        }
        public IActionResult Index()
        {
            int userID;
            userID = HttpContext.Session.GetInt32(Constants.UserId).Value;
            return View(_serviceService.List(userID).Data);

        }

        public IActionResult MyOrderList()
        {
            return View();
        }
        public IActionResult Details(int ID)
        {
            ServiceResult<Service> result = _serviceService.Find(ID);
            if (result.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }
        public IActionResult Create()
        {
            List<Category> categories = _categoryService.List().Data;
            List<SelectListItem> SelectListItems =
                categories.Select(c => new SelectListItem(c.Name, c.ID.ToString())).ToList();
            ViewData["categories"] = SelectListItems;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ServiceViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                ServiceResult<Service> result = _serviceService.Create(model, HttpContext);
                if (!result.IsError)
                    return RedirectToAction(nameof(Index));
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }


            }
            return View(model);



        }
        public IActionResult Edit(int id)
        {
            List<Category> categories = _categoryService.List().Data;
            List<SelectListItem> SelectListItems =
                categories.Select(c => new SelectListItem(c.Name, c.ID.ToString())).ToList();
            ViewData["categories"] = SelectListItems;
            
            ServiceResult<Service> result = _serviceService.Find(id);
            if (result.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ServiceViewModel model = new ServiceViewModel
            {
                Name = result.Data.Name,
                Explanation = result.Data.Explanation,
                Price=result.Data.Price,

            };
            return View(model); ;
        }
        [HttpPost]

        public IActionResult Edit(int id, ServiceViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                ServiceResult<Service> result = _serviceService.Update(id, model, HttpContext);
                if (!result.IsError)
                    return RedirectToAction(nameof(Index));
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }


            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            ServiceResult<Service> result = _serviceService.Find(id);
            if (result.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            ServiceResult<object> result = _serviceService.Remove(id);
            if (!result.IsError)
                return RedirectToAction(nameof(Index));
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return View(_serviceService.Find(id).Data);

        }
    }
}
