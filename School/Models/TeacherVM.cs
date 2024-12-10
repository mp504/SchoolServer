using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace School.Models
{
    public class TeacherVM
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }


        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        public string ApplicationUserId { get; set; }


        // Relationship with Classes
        public ICollection<CourseVM> TeacherCourses { get; set; }  // Many-to-many with classes
    }
}
