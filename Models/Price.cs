using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingService.Models
{
    public class Price
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        [Required]
        public decimal Hour { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        [Required]
        public decimal Day { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}
