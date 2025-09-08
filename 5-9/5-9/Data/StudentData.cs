using StudentManagementApp.Models;

namespace StudentManagementApp.Data;

public static class StudentData
{
    private static List<Student> students = new List<Student>();
    private static Random random = new Random();

    public static void GenerateStudents(int count = 100000)
    {
        string[] firstNames = { "Nguyen", "Tran", "Le", "Pham", "Hoang" };
        string[] middleNames = { "Van", "Thi", "Minh", "Ngoc", "Anh" };
        string[] lastNames = { "An", "Binh", "Cuong", "Dung", "Hanh" };
        string[] classes = { "CNTT1", "CNTT2", "KTPM1", "KTPM2", "HTTT" };

        for (int i = 1; i <= count; i++)
        {
            students.Add(new Student
            {
                Id = i,
                FirstName = firstNames[random.Next(firstNames.Length)],
                MiddleName = middleNames[random.Next(middleNames.Length)],
                LastName = lastNames[random.Next(lastNames.Length)],
                Birthday = DateTime.Now.AddYears(-random.Next(18, 25)),
                EClass = classes[random.Next(classes.Length)],
                Phone = "0" + random.Next(100000000, 999999999).ToString(),
                Email = $"student{i}@example.com"
            });
        }
    }

    public static List<Student> GetStudents(int page, int pageSize, out int totalPages)
    {
        totalPages = (int)Math.Ceiling(students.Count / (double)pageSize);
        return students.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public static void AddStudent(Student student)
    {
        student.Id = students.Count + 1;
        students.Add(student);
    }
}