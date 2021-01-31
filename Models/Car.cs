using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingService.Models
{
    public class Car
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "The string length must be between 3 and 200 characters!")]
        [Required(ErrorMessage ="Enter model!")]
        public string Model { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "The string length must be between 3 and 200 characters!")]
        [Required(ErrorMessage = "Enter color!")]
        public string Color { get; set; }


        [Required(ErrorMessage = "Enter plate number!")]
        public string PlateNumber { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "The string length must be between 3 and 200 characters!")]
        [Required(ErrorMessage = "Enter owner`s name!")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Enter phone number!")]
        [Phone]
        public string PhoneNumber { get; set; }

        public List<Entry> Entries { get; set; }
        public List<Balance> Balances { get; set; }

        [NotMapped]
        public decimal Payment { get; set; }
    }
}
