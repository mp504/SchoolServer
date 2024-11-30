using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerOfSchool.Models
{
    public class Teacher
    {
        [JsonIgnore]
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

        [ForeignKey("ApplicationUser")]
        // Foreign key to ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        // Relationship with Classes
        public ICollection<Course> TeacherCourses { get; set; }  // Many-to-many with classes
    }
}
