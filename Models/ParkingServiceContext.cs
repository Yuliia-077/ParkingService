using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParkingService.Models
{
    public class ParkingServiceContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public ParkingServiceContext(DbContextOptions<ParkingServiceContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
