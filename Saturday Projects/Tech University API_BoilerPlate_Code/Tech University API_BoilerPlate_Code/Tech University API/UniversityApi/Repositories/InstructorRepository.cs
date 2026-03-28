using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Repositories
{
    public class InstructorRepository : IInstructor
    {
        // Implement your code here  
        private readonly UniversityContext universityContext;

        public InstructorRepository(UniversityContext university)
        {
            universityContext = university;
        }

        public bool AddInstructor(Instructor instructor)
        {
            var instr = universityContext.Instructors.Find(instructor.InstructorId);

            if (instr != null) return false;

            universityContext.Instructors.Add(instructor);
            universityContext.SaveChangesAsync();

            return true;
        }
        public IEnumerable<Instructor> GetInstructorsWithCourseCountAbove(int count)
        {
            var res = universityContext.Instructors.Include(i => i.InstructorCourses)
                .Where(x => x.InstructorCourses.Count() > count).ToList();

            return res;
        }
        public IEnumerable<Instructor> GetInstructorsWithMostEnrollments()
        {
            var res = universityContext.Instructors.Include(i => i.InstructorCourses)
                .ThenInclude(i => i.Course).ThenInclude(i => i.Enrollments)
                .Select(i => new
                {
                    Instructor = i,
                    EnrollmentCount = i.InstructorCourses.SelectMany(ic => ic.Course.Enrollments)
                    .Count()
                });
            var max = res.Max(x => x.EnrollmentCount);
            var result = res.Where(i => i.EnrollmentCount == max).Select(i => i.Instructor).ToList();

            return result;
        }


    }
}
