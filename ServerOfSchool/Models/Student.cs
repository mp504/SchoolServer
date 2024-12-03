using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerOfSchool.Models
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
        public DateOnly DateOfBirth { get; set; }


        // Foreign key to ApplicationUser
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public StudentAddress Address { get; set; }

        // Relationship with Courses
        public ICollection<Course> Courses { get; set; }  // Many-to-many with Courses

    }
}
