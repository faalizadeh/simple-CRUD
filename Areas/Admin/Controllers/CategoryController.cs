using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imidro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OnlineShopping.Models.ViewModel;
using OnlineShopping.Repository;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICarRepository _Repository;

        public CategoryController(ICarRepository _Repository)
        {
            this._Repository = _Repository;
        }


        public async Task<IActionResult> Index(string status)
        {
            if (status != null)
            {
                TempData["Message"] = Result.GenerateMessage(Convert.ToBoolean(status));

            }
            else
            {
                TempData["Message"] = "";
            }

            var result = await _Repository.GetAllCategory();
            ViewBag.TitlePage = "دسته بندی ها";
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> AddCategory() => View();


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel vm)
        {

            if (ModelState.IsValid)
            {
                var image = HttpContext.Request.Form.Files;
                vm.formFiles = image;
                var result = await _Repository.AddNewCategory(vm);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id != null)
            {
                var CategoryData = await _Repository.GetCategory(id);
                if (CategoryData.IsAccept)
                {
                    ViewBag.TitlePage = " ویرایش دسته بندی ها";

                    return View(CategoryData.Category);

                }
                else
                {
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = CategoryData.IsAccept }));

                }
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));

        }


        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryViewModel Vm)
        {
            if (ModelState.IsValid)
            {
                var image = HttpContext.Request.Form.Files;
                Vm.formFiles = image;
                var result = await _Repository.UpdateCategory(Vm);
                if (result)
                {
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = true }));

                }
            }
            else
            {
                var CategoryData = await _Repository.GetCategory(Vm.ID);
                if (CategoryData.IsAccept)
                    Vm.ImageUrl = CategoryData.Category.ImageUrl;
            }

            return View(Vm);

        }

        [HttpGet]

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id != null)
            {
                var Data = await _Repository.GetCategory(id);
                if (Data.IsAccept)
                {
                    return View(Data.Category);

                }
                else
                {
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = Data.IsAccept }));
                }

            }
            else
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));

            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(CategoryViewModel viewModel)
        {
            var result = await _Repository.DeleteCategory(viewModel);

            if (result)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { status = true }));

            }
            else
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));

            }

        }
    }
}