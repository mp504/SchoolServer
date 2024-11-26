
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;




namespace ServerOfSchool.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        // Navigation properties to Student or Teacher
        public Student StudentProfile { get; set; }
        public Teacher TeacherProfile { get; set; }
    }
}
