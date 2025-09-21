using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMarksMvc.Data;
using StudentMarksMvc.Models;

namespace StudentMarksMvc.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 10; 

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            var totalStudents = _context.Students.Count();
            var totalPages = (int)Math.Ceiling(totalStudents / (double)PageSize);

            var students = _context.Students
                .Include(s => s.Marks)
                .ThenInclude(m => m.Subject)
                .OrderBy(s => s.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.Subjects = _context.Subjects.ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(students);
        }

        [HttpGet]
        public IActionResult EditStudentAjax(int id)
        {
            var student = _context.Students
                .Include(s => s.Marks)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return Json(new { success = false, message = "Không tìm thấy học sinh" });

            return Json(new
            {
                success = true,
                id = student.Id,
                fullName = student.FullName,
                grade = student.Grade,
                marks = student.Marks.Select(m => new { subjectId = m.SubjectId, score = m.Score })
            });
        }

        [HttpPost]
        public IActionResult CreateStudentAjax([FromBody] Student student)
        {
            if (student == null)
                return Json(new { success = false, message = "Dữ liệu gửi lên không hợp lệ (student null)" });

            if (string.IsNullOrWhiteSpace(student.FullName))
                return Json(new { success = false, message = "Tên học sinh không được để trống" });

            if (string.IsNullOrWhiteSpace(student.Grade))
                return Json(new { success = false, message = "Lớp không được để trống" });

            if (student.Marks != null)
            {
                foreach (var mark in student.Marks)
                {
                    if (!_context.Subjects.Any(s => s.Id == mark.SubjectId))
                        return Json(new { success = false, message = $"Môn học với ID {mark.SubjectId} không tồn tại" });

                    if (mark.Score < 0)
                        return Json(new { success = false, message = "Điểm không được âm" });
                }
            }

            var newStudent = new Student
            {
                FullName = student.FullName,
                Grade = student.Grade,
                Marks = student.Marks?.Select(m => new StudentMark
                {
                    SubjectId = m.SubjectId,
                    Score = m.Score
                }).ToList() ?? new List<StudentMark>()
            };

            _context.Students.Add(newStudent);
            try
            {
                _context.SaveChanges();
                return Json(new { success = true, message = "Thêm học sinh thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi khi lưu dữ liệu: {ex.Message}" });
            }
        }

        [HttpPut]
        public IActionResult EditStudentAjax([FromBody] Student student)
        {
            if (student == null || student.Id == 0)
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

            var existing = _context.Students
                .Include(s => s.Marks)
                .FirstOrDefault(s => s.Id == student.Id);

            if (existing == null)
                return Json(new { success = false, message = "Không tìm thấy học sinh" });

            existing.FullName = student.FullName;
            existing.Grade = student.Grade;

            if (student.Marks != null)
            {
                var marksToRemove = existing.Marks
                    .Where(m => !student.Marks.Any(sm => sm.SubjectId == m.SubjectId))
                    .ToList();
                _context.StudentMarks.RemoveRange(marksToRemove);

                foreach (var mark in student.Marks)
                {
                    if (!_context.Subjects.Any(s => s.Id == mark.SubjectId))
                        return Json(new { success = false, message = $"Môn học với ID {mark.SubjectId} không tồn tại" });

                    if (mark.Score < 0)
                        return Json(new { success = false, message = "Điểm không được âm" });

                    var existingMark = existing.Marks.FirstOrDefault(m => m.SubjectId == mark.SubjectId);
                    if (existingMark != null)
                    {
                        existingMark.Score = mark.Score;
                    }
                    else
                    {
                        existing.Marks.Add(new StudentMark
                        {
                            SubjectId = mark.SubjectId,
                            Score = mark.Score
                        });
                    }
                }
            }

            try
            {
                _context.SaveChanges();
                return Json(new { success = true, message = "Cập nhật học sinh thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi khi lưu dữ liệu: {ex.Message}" });
            }
        }

        [HttpDelete]
        public IActionResult DeleteStudentAjax(int id)
        {
            var student = _context.Students
                .Include(s => s.Marks)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return Json(new { success = false, message = "Không tìm thấy học sinh" });

            _context.StudentMarks.RemoveRange(student.Marks);
            _context.Students.Remove(student);
            try
            {
                _context.SaveChanges();
                return Json(new { success = true, message = "Xóa học sinh thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi khi xóa dữ liệu: {ex.Message}" });
            }
        }
    }
}
