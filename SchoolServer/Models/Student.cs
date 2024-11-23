using System.ComponentModel.DataAnnotations;

namespace SchoolServer.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }




        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        
        public StudentAddress Address { get; set; }

        // Relationship with Courses
        public ICollection<StudentCourses> Courses { get; set; }  // Many-to-many with Courses
    }
}
