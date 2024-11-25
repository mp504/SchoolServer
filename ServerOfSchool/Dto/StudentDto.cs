using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServerOfSchool.Dto
{
    public class StudentDto
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


        public StudentAddressDto Address { get; set; }

        



    }
}
