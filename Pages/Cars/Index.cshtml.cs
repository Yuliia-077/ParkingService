using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ParkingService.ViewModels;

namespace ParkingService.Pages.Cars
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        public List<Models.Car> Cars { get; set; }
        public string Message { get; set; }
        
        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Cars.Any())
            {
                Cars = await _context.Cars.OrderByDescending(p => p.Id).ToListAsync();
                if (Cars != null)
                {
                    return Page();
                }
            }
            return RedirectToPage("./Create");
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            Car car = await _context.Cars.Include(x => x.Entries).Include(y => y.Balances).FirstOrDefaultAsync(p => p.Id == id);
            if (car != null)
            {
                Counting counting = new Counting(_context);
                car = await counting.Debts(car);
                if(car.Payment < 0)
                {
                    Message = "Debt in the amount of " + Convert.ToString(car.Payment);
                }
                else
                {
                    _context.Cars.Remove(car);
                    _context.SaveChanges();
                }
            }
            Cars = await _context.Cars.OrderByDescending(p => p.Id).ToListAsync();
            return Page();
        }
    }
}
