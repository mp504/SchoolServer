    using System.ComponentModel.DataAnnotations;

    namespace School.Models
    { 
            public class LoginModel
            {
                [Required]
                [EmailAddress]
                [Display(Name = "Email")]
            public string Email { get; set; }

                [Required]
     
            public string Password { get; set; }
        }
}
