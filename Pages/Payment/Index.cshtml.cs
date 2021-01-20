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
        public List<Entry> Entries { get; set; }

        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public async Task<IActionResult> OnGet()
        {
            if(await _context.Entries.AnyAsync())
            {
                var list = await _context.Entries.OrderByDescending(n => n.Id).ToListAsync();
                List<Entry> entries = new List<Entry>();
                foreach(var item in list)
                {
                    Entry entry = item;
                    entry.Car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == item.CarId);

                    Counting counting = new Counting(_context);
                    entry = await counting.Debts(entry);

                    entries.Add(entry);
                }
                Entries = entries;
                return Page();

            }
            return RedirectToPage("/Journal/Create");
            
        }
    }
}
