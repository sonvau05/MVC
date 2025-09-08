using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;
using StudentManagementApp.Data;

namespace StudentManagementApp.Controllers;

public class StudentController : Controller
{
    private const int PageSize = 10;

    public StudentController()
    {
        if (!StudentData.GetStudents(1, 1, out _).Any())
            StudentData.GenerateStudents();
    }

    public IActionResult Index(int page = 1)
    {
        var students = StudentData.GetStudents(page, PageSize, out int totalPages);
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        return View(students);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        var (isValid, errorMessage) = StudentValidator.Validate(student);
        if (!isValid)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View(student);
        }

        StudentData.AddStudent(student);
        return RedirectToAction("Index");
    }
}