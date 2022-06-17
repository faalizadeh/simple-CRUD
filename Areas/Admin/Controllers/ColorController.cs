using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OnlineShopping.Models.ViewModel;
using OnlineShopping.Repository;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly ICarRepository _Repository;


        public ColorController(ICarRepository Repository)
        {
            _Repository = Repository;
        }
        public async Task<IActionResult> Index()
        {
            var result=await _Repository.GetAllColors();
            ViewBag.TitlePage = "رنگ ها";

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddColor() => View();

        [HttpPost]
        public async Task<IActionResult> AddColor(ColorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _Repository.AddNewColor(vm);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));
            }
            return View(vm);
        }


        
        [HttpGet]
        public async Task<IActionResult> EditColor(int ?id )
        {
            if(id!=null)
            {
                var result = await _Repository.GetColor(id);

                if(result.IsAccept)
                {
                    return View(result.Color);
                }else
                {
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = result.IsAccept }));
                }

            }   

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditColor(ColorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result=await _Repository.UpdateColor(viewModel);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));    
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> DeleteColor(int? id)
        {
            if (id != null)
            {
                var ColorData = await _Repository.GetColor(id);
                if (ColorData.IsAccept)
                {
                    return View(ColorData.Color);
                }
                else
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = ColorData.IsAccept }));

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteColor(ColorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _Repository.DeleteColor(viewModel);
                return RedirectToAction("Index", new RouteValueDictionary(new { status = result }));

            }
            return View();
        }
    }
}