using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<InstructorCourse> InstructorCourses { get; set; }
    }
}
