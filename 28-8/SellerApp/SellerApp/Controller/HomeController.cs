using Microsoft.AspNetCore.Mvc;
using SellerApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SellerApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Điện thoại" },
                new Category { Id = 2, Name = "Laptop" }
            };

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "iPhone 8", Price = 10000000, Description = "Điện thoại Apple cũ", ImageUrl = "https://via.placeholder.com/150?text=iPhone8", CategoryId = 1 },
                new Product { Id = 2, Name = "iPhone 1", Price = 5000000, Description = "Phiên bản cổ điển", ImageUrl = "https://via.placeholder.com/150?text=iPhone1", CategoryId = 1 },
                new Product { Id = 3, Name = "Android", Price = 8000000, Description = "Điện thoại phổ thông", ImageUrl = "https://via.placeholder.com/150?text=Android", CategoryId = 1 },
                new Product { Id = 4, Name = "Samsung Galaxy", Price = 12000000, Description = "Màn hình lớn", ImageUrl = "https://via.placeholder.com/150?text=Galaxy", CategoryId = 1 },
                new Product { Id = 5, Name = "M1", Price = 20000000, Description = "Laptop Apple M1", ImageUrl = "https://via.placeholder.com/150?text=M1", CategoryId = 2 },
                new Product { Id = 6, Name = "M2", Price = 25000000, Description = "Phiên bản nâng cấp", ImageUrl = "https://via.placeholder.com/150?text=M2", CategoryId = 2 },
                new Product { Id = 7, Name = "M3", Price = 30000000, Description = "Hiệu suất cao", ImageUrl = "https://via.placeholder.com/150?text=M3", CategoryId = 2 },
                new Product { Id = 8, Name = "M4", Price = 35000000, Description = "Mới nhất", ImageUrl = "https://via.placeholder.com/150?text=M4", CategoryId = 2 }
            };

            var groupedProducts = products.GroupBy(p => p.CategoryId)
                                          .Select(g => new
                                          {
                                              Category = categories.First(c => c.Id == g.Key),
                                              Products = g.ToList()
                                          });

            ViewBag.GroupedProducts = groupedProducts;
            return View();
        }
    }
}