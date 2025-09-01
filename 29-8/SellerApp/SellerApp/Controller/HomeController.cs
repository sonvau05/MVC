using Microsoft.AspNetCore.Mvc;
using SellerApp.Models;
using System.Linq;

namespace SellerApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Categories = DataStore.Categories,
                Products = DataStore.Products
            };
            return View(model);
        }

        public IActionResult Category(int id)
        {
            var category = DataStore.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var products = DataStore.Products.Where(p => p.CategoryId == id).ToList();
            var model = new
            {
                Category = category,
                Products = products
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}