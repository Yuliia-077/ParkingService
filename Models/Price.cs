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
        [Required(ErrorMessage ="Enter one hour price!")]
        [DataType(DataType.Currency)]
        public decimal Hour { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        [Required(ErrorMessage = "Enter one day price!")]
        public decimal Day { get; set; }

        [Required(ErrorMessage = "Enter date and time!")]
        public DateTime DateTime { get; set; }
    }
}
