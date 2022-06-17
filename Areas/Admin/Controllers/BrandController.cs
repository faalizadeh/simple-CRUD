using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using imidro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using OnlineShopping.Models;
using OnlineShopping.Models.ViewModel;
using OnlineShopping.Repository;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {

        private readonly ICarRepository _Repository;


        public BrandController(ICarRepository _Repository)
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
            var result = await _Repository.GetAllBrands();
            ViewBag.TitlePage = "برند ها";
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddBrand() => View();


        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandViewModel vm, IFormFile file)
        {
            //IFormFile file baray ravesh 2

            if (ModelState.IsValid)
            {
                //ravesh 1

                //string base64 = "";
                //using (MemoryStream obj=new MemoryStream()) 
                //{
                //    file.CopyTo(obj);
                //    byte[] Arrays = obj.ToArray();
                //    base64 = Convert.ToBase64String(Arrays);
                //}

                //string image = string.Format("data:image/jpeg;base64,{0}",base64);


                //ravesh 2
                var image = HttpContext.Request.Form.Files;
                vm.formFiles = image;

                var result = await _Repository.AddNewBrand(vm);

                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));

            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditBrand(int? id)
        {
            if (id != null)
            {
                var BrandData = await _Repository.GetBrand(id);
                if (BrandData.IsAccept)
                {
                    return View(BrandData.brand);
                }
                else
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = BrandData.IsAccept }));

            }
            return RedirectToAction("Index", new RouteValueDictionary(new { status = false }));
        }


        [HttpPost]
        public async Task<IActionResult> EditBrand(BrandViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var image = HttpContext.Request.Form.Files;
                vm.formFiles = image;
                var result = await _Repository.UpdateBrand(vm);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));

            }
            else
            {
                var BrandData = await _Repository.GetBrand(vm.ID);
                if (BrandData.IsAccept)
                {

                    vm.ImageUrl = BrandData.brand.ImageUrl;
                }
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBrand(int? id)
        {
            if (id != null)
            {
                var BrandData = await _Repository.GetBrand(id);
                if (BrandData.IsAccept)
                {
                    return View(BrandData.brand);
                }
                else
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = BrandData.IsAccept }));

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBrand(BrandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _Repository.DeleteBrand(viewModel);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));

            }
            return View();
        }


    }
}