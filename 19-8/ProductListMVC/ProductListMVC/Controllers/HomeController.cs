using Microsoft.AspNetCore.Mvc;
using ProductListMVC.Models;
using System.Collections.Generic;

public class HomeController : Controller
{
    private static readonly List<Product> Products = new List<Product>
         {
             new Product { Id = 1, Name = "Laptop", Price = 1000 },
             new Product { Id = 2, Name = "Phone", Price = 500 },
             new Product { Id = 3, Name = "Tablet", Price = 300 }
         };

    public IActionResult Index()
    {
        return View(Products);
    }

    public IActionResult Details(int id)
    {
        var product = Products.Find(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
}