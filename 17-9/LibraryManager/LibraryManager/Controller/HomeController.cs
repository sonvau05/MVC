using Microsoft.AspNetCore.Mvc;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, string searchString = "")
        {
            int pageSize = 5;
            var books = _context.Books.Include(b => b.Category).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
            }
            int totalBooks = books.Count();
            int totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));
            var paginatedBooks = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Borrows = _context.Borrows.Include(b => b.Book).ToList();
            return View(paginatedBooks);
        }
    }
}