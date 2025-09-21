using Microsoft.AspNetCore.Mvc;
using StudentMarksMvc.Data;
using StudentMarksMvc.Models;

namespace StudentMarksMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Subject subject)
        {
            if (string.IsNullOrWhiteSpace(subject.Name))
                return BadRequest(new { success = false, message = "Tên môn học không hợp lệ" });

            _context.Subjects.Add(subject);
            _context.SaveChanges();

            return Ok(subject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Subject subject)
        {
            var existing = _context.Subjects.Find(id);
            if (existing == null)
                return NotFound(new { success = false, message = "Không tìm thấy môn học" });

            existing.Name = subject.Name;
            _context.SaveChanges();

            return Ok(new { success = true });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null)
                return NotFound(new { success = false, message = "Không tìm thấy môn học" });

            _context.Subjects.Remove(subject);
            _context.SaveChanges();

            return Ok(new { success = true });
        }
    }
}
