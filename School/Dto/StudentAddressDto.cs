﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServerOfSchool.Dto
{
    public class StudentAddressDto
    {
       
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
    }
}