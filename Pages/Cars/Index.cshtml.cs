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
            Car car = await _context.Cars.FirstOrDefaultAsync(p => p.Id == id);
            if (car != null)
            {
                List<Entry> entries = await _context.Entries.Where(k=>k.CarId == car.Id).ToListAsync();
                decimal sumDebts = 0;
                foreach(Entry en in entries)
                {
                    Counting counting = new Counting(_context);
                    Entry entry = await counting.Debts(en);
                    if(entry.IsPaid == false)
                    {
                        sumDebts += entry.Payment;
                    }
                }
                if(sumDebts == 0)
                {
                    _context.Cars.Remove(car);
                    _context.SaveChanges();
                }
                else
                {
                    Message = "Debt in the amount of " + Convert.ToString(sumDebts);
                }
            }
            Cars = await _context.Cars.OrderByDescending(p => p.Id).ToListAsync();
            return Page();
        }
    }
}
