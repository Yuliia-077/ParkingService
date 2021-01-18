using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingService.Models
{
    public class Balance
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        [Required]
        public decimal Payment { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int EntryId { get; set; }
        public Entry Entry { get; set; }
    }
}
