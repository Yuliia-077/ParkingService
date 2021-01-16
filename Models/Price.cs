using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class Price
    {
        public int Id { get; set; }
        public decimal Hour { get; set; }
        public decimal Day { get; set; }
        public DateTime DateTime { get; set; }
    }
}
