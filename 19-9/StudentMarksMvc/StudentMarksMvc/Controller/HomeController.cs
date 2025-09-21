using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMarksMvc.Data;
using StudentMarksMvc.Models;
using System.Linq;

namespace StudentMarksMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 10; 

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search, string filter, int page = 1)
        {
            var query = _context.Students
                .Include(s => s.Marks)
                .ThenInclude(m => m.Subject)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.FullName.Contains(search));
            }

            if (filter == "top")
            {
                query = query.OrderByDescending(s => s.Marks.Any() ? s.Marks.Average(m => m.Score) : 0);
            }
            else if (filter == "low")
            {
                query = query.OrderBy(s => s.Marks.Any() ? s.Marks.Average(m => m.Score) : 0);
            }
            else
            {
                query = query.OrderBy(s => s.Id);
            }

            var totalStudents = query.Count();
            var totalPages = (int)Math.Ceiling(totalStudents / (double)PageSize);

            var students = query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var subjects = _context.Subjects.ToList();

            var averageScore = _context.StudentMarks.Any()
                ? _context.StudentMarks.Average(m => m.Score)
                : 0;
            var highAchievers = query.Count(s => s.Marks.Any() && s.Marks.Average(m => m.Score) >= 9);
            var lowCount = query.Count(s => s.Marks.Any() && s.Marks.Average(m => m.Score) < 5);

            var topStudent = query
                .OrderByDescending(s => s.Marks.Any() ? s.Marks.Average(m => m.Score) : 0)
                .FirstOrDefault();

            var lowStudent = query
                .OrderBy(s => s.Marks.Any() ? s.Marks.Average(m => m.Score) : 0)
                .FirstOrDefault();

            ViewBag.Subjects = subjects;
            ViewBag.TotalStudents = totalStudents;
            ViewBag.AverageScore = averageScore;
            ViewBag.HighAchievers = highAchievers;
            ViewBag.LowCount = lowCount;
            ViewBag.TopStudent = topStudent;
            ViewBag.LowStudent = lowStudent;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.Search = search;
            ViewBag.Filter = filter;

            return View(students);
        }
    }
}