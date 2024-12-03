using ServerOfSchool.Dto;
using System.ComponentModel.DataAnnotations;

namespace ServerOfSchool.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // "Student" or "Teacher"

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }
        public StudentAddressDto address {  get; set; } 
    }
}
