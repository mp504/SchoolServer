using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServerOfSchool.Dto
{
    public class CourseDto
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; } // Class Name (e.g., Math, Science)
    }
}
