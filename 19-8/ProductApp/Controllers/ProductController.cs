using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class ProductController : Controller
{
    private static readonly List<Product> Products = new()
    {
        new() { Id = 1, Name = "Laptop", Price = 999.99m },
        new() { Id = 2, Name = "Phone", Price = 599.99m }
    };

    public IActionResult Index() => View(Products);

    public IActionResult Details(int id)
    {
        var product = Products.Find(p => p.Id == id);
        return product != null ? View(product) : NotFound();
    }
}