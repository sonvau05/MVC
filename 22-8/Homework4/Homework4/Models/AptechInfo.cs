namespace MvcEmptyApp.Models
{
    public class AptechInfo
    {
        public string Address { get; set; } = "123 Đường Láng, Hà Nội";
        public string Phone { get; set; } = "0123 456 789";
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
    }
}