using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? LeavingTime { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
