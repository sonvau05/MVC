using Microsoft.AspNetCore.Mvc;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Controllers
{
    public class BorrowsController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: /Borrows
        public IActionResult Index()
        {
            var borrows = _context.Borrows
                .Include(b => b.Book)
                .ToList();
            return View(borrows);
        }

        // GET: /Borrows/Details/5
        public IActionResult Details(int id)
        {
            var borrow = _context.Borrows
                .Include(b => b.Book)
                .FirstOrDefault(b => b.Id == id);
            if (borrow == null) return NotFound();
            return View(borrow);
        }

        // GET: /Borrows/Create
        public IActionResult Create()
        {
            var books = _context.Books.ToList();
            if (!books.Any())
            {
                ViewBag.ErrorMessage = "Không có sách nào trong hệ thống. Vui lòng thêm sách trước.";
            }
            ViewBag.Books = new SelectList(books, "Id", "Title");
            return View();
        }

        // POST: /Borrows/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Borrows.Add(borrow);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi thêm mượn sách: " + ex.Message);
                }
            }
            var books = _context.Books.ToList();
            if (!books.Any())
            {
                ViewBag.ErrorMessage = "Không có sách nào trong hệ thống. Vui lòng thêm sách trước.";
            }
            ViewBag.Books = new SelectList(books, "Id", "Title", borrow.BookId);
            return View(borrow);
        }

        // GET: /Borrows/Edit/5
        public IActionResult Edit(int id)
        {
            var borrow = _context.Borrows.Find(id);
            if (borrow == null) return NotFound();

            var books = _context.Books.ToList();
            if (!books.Any())
            {
                ViewBag.ErrorMessage = "Không có sách nào trong hệ thống. Vui lòng thêm sách trước.";
            }
            ViewBag.Books = new SelectList(books, "Id", "Title", borrow.BookId);
            return View(borrow);
        }

        // POST: /Borrows/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrow);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi sửa mượn sách: " + ex.Message);
                }
            }
            var books = _context.Books.ToList();
            if (!books.Any())
            {
                ViewBag.ErrorMessage = "Không có sách nào trong hệ thống. Vui lòng thêm sách trước.";
            }
            ViewBag.Books = new SelectList(books, "Id", "Title", borrow.BookId);
            return View(borrow);
        }

        // GET: /Borrows/Delete/5
        public IActionResult Delete(int id)
        {
            var borrow = _context.Borrows
                .Include(b => b.Book)
                .FirstOrDefault(b => b.Id == id);
            if (borrow == null) return NotFound();
            return View(borrow);
        }

        // POST: /Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var borrow = _context.Borrows.Find(id);
            if (borrow == null) return NotFound();

            try
            {
                _context.Borrows.Remove(borrow);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi xóa mượn sách: " + ex.Message);
                return View(borrow);
            }
        }
    }

}