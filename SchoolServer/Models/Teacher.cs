using System.ComponentModel.DataAnnotations;

namespace SchoolServer.Models
{
    public class Teacher
    {

        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        // Relationship with Classes
        public ICollection<Course> TeacherCourses { get; set; }  // Many-to-many with classes
    }
}
