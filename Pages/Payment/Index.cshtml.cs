using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.Models;
using ParkingService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ParkingService.Pages.Payment
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        public List<Car> Cars { get; set; }

        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public async Task<IActionResult> OnGet()
        {
            if(await _context.Entries.AnyAsync())
            {
                // var list = await _context.Entries.OrderByDescending(n => n.Id).ToListAsync();
                var list = await _context.Cars.Include(x => x.Entries).Include(y => y.Balances).ToListAsync();
                List<Car> cars = new List<Car>();
                foreach(var item in list)
                {
                    Car car = item;
                    Counting counting = new Counting(_context);
                    car = await counting.Debts(car);
                    cars.Add(car);
                }
                Cars = cars;
                return Page();

            }
            return RedirectToPage("/Journal/Create");
            
        }
    }
}
