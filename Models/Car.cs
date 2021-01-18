using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ParkingService.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string PlateNumber { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
