﻿using System;
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
        [Required(ErrorMessage = "Enter payment!")]
        public decimal Payment { get; set; }
        [Required(ErrorMessage = "Enter date and time!")]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
