using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServerOfSchool.Models
{
    public class Course
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; } // Class Name (e.g., Math, Science)



        // Relationship with Teachers
        [JsonIgnore]
        public ICollection<Teacher> TeacherCourses { get; set; }  // Many-to-many with teachers

        // Relationship with Students
        [JsonIgnore]
        public ICollection<Student> StudentCourses { get; set; }  // Many-to-many with students
    }
}
