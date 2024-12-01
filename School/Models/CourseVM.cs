using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class CourseVM
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [JsonProperty("courseName")]
        public string CourseName { get; set; } // Class Name (e.g., Math, Science)



        // Relationship with Teachers

        public ICollection<TeacherVM> TeacherCourses { get; set; }  // Many-to-many with teachers

        // Relationship with Students

        public ICollection<StudentVM> StudentCourses { get; set; }  // Many-to-many with students
    }
}
