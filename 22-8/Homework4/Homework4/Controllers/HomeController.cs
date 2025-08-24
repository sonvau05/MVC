using Microsoft.AspNetCore.Mvc;
using MvcEmptyApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcEmptyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Student> students = new()
        {
            new Student { Id = 1, Name = "Nguyễn Văn A", Age = 20, Major = "CNTT" },
            new Student { Id = 2, Name = "Trần Thị B", Age = 19, Major = "Thiết kế" }
        };

        private readonly List<City> cities = new()
        {
            new City { Name = "Hà Nội" },
            new City { Name = "TP. Hồ Chí Minh" },
            new City { Name = "Đà Nẵng" },
            new City { Name = "Cần Thơ" }
        };

        public IActionResult Index()
        {
            return View();
        }

        [Route("aptech")]
        public IActionResult Aptech()
        {
            var info = new AptechInfo();
            return View(info);
        }

        [Route("aptech/student")]
        public IActionResult StudentList()
        {
            return View(students);
        }

        [Route("aptech/student/{id}")]
        public IActionResult StudentDetail(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        [Route("Vietnam/thanhpho")]
        public IActionResult CityList()
        {
            return View(cities);
        }
    }
}