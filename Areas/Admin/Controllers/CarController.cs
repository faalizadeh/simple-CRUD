using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imidro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using OnlineShopping.Models.ViewModel;
using OnlineShopping.Repository;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ICarRepository _Repository;

        public CarController(ICarRepository _Repository)
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

            var car = await _Repository.GetAllCar();
            ViewBag.TitlePage = "ماشین ها";
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> AddnewCar()
        {
            List<BrandViewModel> brands = await _Repository.GetAllBrands();
            List<CategoryViewModel> categories = await _Repository.GetAllCategory();
            //List<ModelViewModel> models = await _Repository.GetAllModels();

            CarViewModel vm = new CarViewModel();
            vm.BrandList = new SelectList(brands, "ID", "BrandName");
            //vm.ModelList = new SelectList(models, "ID", "ModelName");
            vm.CategoryList = new SelectList(categories, "ID", "CategoryName");
            ViewBag.TitlePage = " افزودن ماشین جدید  ";

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddnewCar(CarViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var result = await _Repository.AddnewCar(viewModel);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));

            }
            else
            {
                List<BrandViewModel> brands = await _Repository.GetAllBrands();
                List<CategoryViewModel> categories = await _Repository.GetAllCategory();

                viewModel.BrandList = new SelectList(brands, "ID", "BrandName");
                viewModel.CategoryList = new SelectList(categories, "ID", "CategoryName");
            }

            return View(viewModel);


        }


        [HttpGet]
        public async Task<IActionResult> EditCar(int? id)
        {
            if (id != null)
            {
                var CarData = await _Repository.GetCar(id);
                if (CarData.IsAccept)
                {
                    List<BrandViewModel> brands = await _Repository.GetAllBrands();
                    List<CategoryViewModel> categories = await _Repository.GetAllCategory();
                    //List<ModelViewModel> models = await _Repository.GetAllModels();
                    CarData.Car.BrandList = new SelectList(brands, "ID", "BrandName");
                    //CarData.Car.ModelList = new SelectList(models, "ID", "ModelName");
                    CarData.Car.CategoryList = new SelectList(categories, "ID", "CategoryName");
                    return View(CarData.Car);
                }
                else
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = CarData.IsAccept }));

            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> EditCar(CarViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var image = HttpContext.Request.Form.Files;
                vm.ImageCar = image;
                var result = await _Repository.UpdateCar(vm);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));

            }
            else
            {
                var CarData = await _Repository.GetCar(vm.ID);
                if (CarData.IsAccept)
                {
                    List<BrandViewModel> brands = await _Repository.GetAllBrands();
                    List<CategoryViewModel> categories = await _Repository.GetAllCategory();
                    CarData.Car.BrandList = new SelectList(brands, "ID", "BrandName");
                    CarData.Car.CategoryList = new SelectList(categories, "ID", "CategoryName");
                    return View(CarData.Car);
                }
                else
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = CarData.IsAccept }));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCar(int? id)
        {
            if (id != null)
            {
                var CarData = await _Repository.GetCar(id);
                if (CarData.IsAccept)
                {
                    List<BrandViewModel> brands = await _Repository.GetAllBrands();
                    List<CategoryViewModel> categories = await _Repository.GetAllCategory();
                    //List<ModelViewModel> models = await _Repository.GetAllModels();
                    CarData.Car.BrandList = new SelectList(brands, "ID", "BrandName");
                    //CarData.Car.ModelList = new SelectList(models, "ID", "ModelName");
                    CarData.Car.CategoryList = new SelectList(categories, "ID", "CategoryName");
                    return View(CarData.Car);
                }
                else
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = CarData.IsAccept }));

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(CarViewModel viewModel)
        {
            var result = await _Repository.DeleteCar(viewModel);
            return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));

            return View();
        }


    }
}