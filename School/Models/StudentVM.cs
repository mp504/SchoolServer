using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class StudentVM
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
        public DateOnly DateOfBirth { get; set; }
        public string ApplicationUserId { get; set; }

        public StudentAddressVM Address { get; set; }

        // Relationship with Courses
        public ICollection<CourseVM> Courses { get; set; }  // Many-to-many with Courses
        
    }
}
