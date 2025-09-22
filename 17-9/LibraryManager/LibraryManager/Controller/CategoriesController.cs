using LibraryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly LibraryContext _context;

        public CategoriesController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category c)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(c);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(c);
        }

        public IActionResult Edit(int id)
        {
            var c = _context.Categories.Find(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category c)
        {
            if (ModelState.IsValid)
            {
                _context.Update(c);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(c);
        }

        public IActionResult Delete(int id)
        {
            var c = _context.Categories.Find(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var c = _context.Categories.Find(id);
            if (c != null)
            {
                _context.Categories.Remove(c);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CreateAjax(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Json(new { success = false });

            var category = new Category { Name = name };
            _context.Categories.Add(category);
            _context.SaveChanges();

            return Json(new { success = true, data = new { category.Id, category.Name } });
        }

        [HttpPost]
        public IActionResult EditAjax(int id, string name)
        {
            var c = _context.Categories.Find(id);
            if (c == null) return Json(new { success = false });

            c.Name = name;
            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult DeleteAjax(int id)
        {
            var c = _context.Categories.Find(id);
            if (c == null) return Json(new { success = false });

            _context.Categories.Remove(c);
            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}
