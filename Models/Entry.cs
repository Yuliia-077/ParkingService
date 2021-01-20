using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingService.Models
{
    public class Entry
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter entry time!")]
        public DateTime EntryTime { get; set; }
        public DateTime? LeavingTime { get; set; }

        [Required(ErrorMessage = "Choose car!")]
        public int CarId { get; set; }
        public Car Car { get; set; }
        public List<Balance> Balances { get; set; }

        [NotMapped]
        public decimal Payment { get; set; }
        [NotMapped]
        public bool IsPaid { get; set; }

    }
}
