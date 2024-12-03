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
    }
}
