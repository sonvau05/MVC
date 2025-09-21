using System.Collections.Generic;

namespace StudentMarksMvc.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;

        public ICollection<StudentMark> Marks { get; set; } = new List<StudentMark>();
    }
}
