using UniversityApi.Models;

namespace UniversityApi.Interfaces
{
    public interface ICourse
    {
        // Implement your code here
        bool UpdateCourse(Course course);
        IEnumerable<Course> GetCoursesWithEnrollmentsAboveGrade(int grade);
        IEnumerable<Course> GetCoursesByInstructorName(string instructorName);
    }
}
