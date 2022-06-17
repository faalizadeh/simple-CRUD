using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ICarRepository _Repository;

        public ProductController(ICarRepository _Repository)
        {
            this._Repository = _Repository;
        }
        public async Task<IActionResult> Index()
        {
            var car = await _Repository.GetAllCar();

            return View(car);
        }

        public async Task<IActionResult> ShowDetail(int id)
        {
            if (id != null)
            {
                var CarData = await _Repository.GetCar(id);
                if (CarData.IsAccept)
                {

                    return View(CarData.Car);
                }
                else
                    return RedirectToAction("Index", new RouteValueDictionary(new { status = CarData.IsAccept }));

            }
            return RedirectToAction("Index");
        }
    }
}
