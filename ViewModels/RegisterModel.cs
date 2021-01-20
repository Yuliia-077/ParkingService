using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ParkingService.ViewModels
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 6, ErrorMessage = "The string length must be between 6 and 200 characters!")]
        [Required(ErrorMessage = "Enter name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Email!")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password!")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The string length must be between 6 and 50 characters!")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}
