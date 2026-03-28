using UniversityApi.Models;

namespace UniversityApi.Interfaces
{
    public interface IStudent
    {
        // Implement your code here  
        bool DeleteStudent(int studentId);

        IEnumerable<Student> GetStudentsByCourseTitle(string courseTitle);
    }
}
