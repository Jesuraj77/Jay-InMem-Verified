using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EMartV2.Web.Models;
using EMartV2.BuisnessLayer.Interfaces;
using EMartV2.Models.ProductModels;

namespace EMartV2.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _service;
        public HomeController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var productId = RandomNumber(1, 999999);
            var productName = "Ps4_" + productId;
            var productTest = new Product { Id = productId, Name = productName };
            await _service.CreateProductAsync(productTest);

            return View();
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public async Task<JsonResult> About()
        {
            var products = await _service.GetAllProductsAsync();

            return Json(products);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
