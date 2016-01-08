using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Models.UserViewModel
{
    public class RegisterUserModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Most than MaxLength[20]")]
        [MinLength(3, ErrorMessage = "Less than MinLength[3]")]
        public string Login { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Most than MaxLength[20]")]
        [MinLength(3, ErrorMessage = "Less than MinLength[3]")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Most than MaxLength[20]")]
        [MinLength(3, ErrorMessage = "Less than MinLength[3]")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Most than MaxLength[20]")]
        [MinLength(3, ErrorMessage = "Less than MinLength[3]")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Most than MaxLength[20]")]
        [MinLength(3, ErrorMessage = "Less than MinLength[3]")]
        public string Email { get; set; }
    }
}