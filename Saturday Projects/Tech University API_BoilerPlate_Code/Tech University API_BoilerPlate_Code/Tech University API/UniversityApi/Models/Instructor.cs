using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models
{
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public ICollection<InstructorCourse> InstructorCourses { get; set; }
    }
}
