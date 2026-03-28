using UniversityApi.Data;
using UniversityApi.Interfaces;
using UniversityApi.Models;
using Microsoft.EntityFrameworkCore;
namespace UniversityApi.Repositories
{
    public class CourseRepository : ICourse
    {
        // Implement your code here  
        private readonly UniversityContext universityContext;

        public CourseRepository(UniversityContext university)
        {
            universityContext = university;
        }

        public bool UpdateCourse(Course course)
        {
            var course1 = universityContext.Courses.Find(course.CourseId);
            if (course1 != null)
            {
                course1.Title = course.Title;
                course1.InstructorCourses = course.InstructorCourses;
                course1.Enrollments = course.Enrollments;
                return true;
            }
            return false;
        }
        public IEnumerable<Course> GetCoursesWithEnrollmentsAboveGrade(int grade)
        {
            var res = universityContext.Courses.Include(c => c.Enrollments)
                .Where(x => x.Enrollments.Any(y=>y.Grade>grade)).ToList();
            return res;
        }
        public IEnumerable<Course> GetCoursesByInstructorName(string instructorName)
        {
            var res = universityContext.InstructorCourses.Include(x => x.Course)
                .Include(y => y.Instructor).Where(x => x.Instructor.Name == instructorName)
                .Select(i => i.Course).ToList();

            return res;
        }


    }
}
