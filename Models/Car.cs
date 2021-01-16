using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string PlateNumber { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber { get; set; }

        public List<Balance> Balances { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
