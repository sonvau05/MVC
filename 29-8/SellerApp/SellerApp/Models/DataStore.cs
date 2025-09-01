using System.Collections.Generic;

namespace SellerApp.Models
{
    public static class DataStore
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại" },
            new Category { Id = 2, Name = "Laptop" }
        };

        public static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 14", Price = 20000000, ImageUrl = "/images/iphone14.png", Description = "iPhone 14 128GB", CategoryId = 1 },
            new Product { Id = 2, Name = "Samsung Galaxy S23", Price = 18000000, ImageUrl = "/images/samsung.png", Description = "Samsung Galaxy S23 256GB", CategoryId = 1 },
            new Product { Id = 3, Name = "MacBook Pro", Price = 35000000, ImageUrl = "/images/macbook.png", Description = "MacBook Pro M1 512GB", CategoryId = 2 }
        };
    }
}