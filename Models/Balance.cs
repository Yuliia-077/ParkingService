using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public decimal Payment { get; set; }
        public DateTime Time { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
