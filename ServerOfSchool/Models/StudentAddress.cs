using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServerOfSchool.Models
{
        public class StudentAddress
        {
                [JsonIgnore]
                public int Id { get; set; }

                [Required]
                [StringLength(200)]
                public string Street { get; set; }

                [Required]
                [StringLength(100)]
                public string City { get; set; }

                [Required]
                [StringLength(50)]
                public string Country { get; set; }

        // Navigation property back to Student
        [JsonIgnore]
        public int AddressOfStudentId { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }  // One Address corresponds to one Student

        }
}
