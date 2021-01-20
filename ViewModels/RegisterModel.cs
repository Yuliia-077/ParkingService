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

        [StringLength(200, MinimumLength = 3, ErrorMessage = "The string length must be between 6 and 200 characters!")]
        [Required(ErrorMessage = "Enter name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Email!")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{6,50}$", ErrorMessage = "Password must have one lowercase, one uppercase and one digit from 0-9 and the string length must be between 6 and 50 characters!")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }
    }
}
