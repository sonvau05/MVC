namespace StudentManagementApp.Models;

public class StudentValidator
{
    public static (bool IsValid, string ErrorMessage) Validate(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.FirstName) ||
            string.IsNullOrWhiteSpace(student.LastName) ||
            string.IsNullOrWhiteSpace(student.EClass))
            return (false, "FirstName, LastName, EClass cannot be empty");

        if (!IsValidEmail(student.Email))
            return (false, "Invalid email format");

        if (!IsValidPhone(student.Phone))
            return (false, "Invalid phone format (10 digits)");

        if (student.Birthday > DateTime.Now || student.Birthday < new DateTime(1900, 1, 1))
            return (false, "Invalid birthday");

        return (true, "");
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidPhone(string phone)
    {
        return phone?.Length == 10 && phone.All(char.IsDigit);
    }
}