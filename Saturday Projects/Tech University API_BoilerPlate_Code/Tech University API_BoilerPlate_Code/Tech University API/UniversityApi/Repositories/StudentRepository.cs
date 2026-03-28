using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Repositories
{
    public class StudentRepository : IStudent
    {
        // Implement your code here
        private readonly UniversityContext universityContext;

        public StudentRepository(UniversityContext university)
        {
            universityContext = university;
        }
        public bool DeleteStudent(int studentId)
        {
            var student = universityContext.Students.Find(studentId);

            if (student is null) return false;

            universityContext.Students.Remove(student);
            universityContext.SaveChangesAsync();
            return true;
        }
        public IEnumerable<Student> GetStudentsByCourseTitle(string courseTitle)
        {
            var res = universityContext.Enrollments.Include(i => i.Course).Include(i => i.Student)
                .Where(e => e.Course.Title == courseTitle).Select(e=>e.Student).Distinct().ToList();

            return res;
        }
    }
}
