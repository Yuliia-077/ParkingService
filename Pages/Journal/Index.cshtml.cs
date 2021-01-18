using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkingService.Models;

namespace ParkingService.Pages.Journal
{
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        public List<Entry> Entries { get; set; }

        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public IActionResult OnGet()
        {
            if(_context.Entries.Any())
            {
                var list = from entry in _context.Entries
                           join car in _context.Cars on entry.CarId equals car.Id
                           orderby entry.Id descending
                           select new
                           {
                               entry,
                               car
                           };

                if (list != null)
                {
                    List<Entry> entries = new List<Entry>();
                    foreach (var c in list)
                    {
                        Entry entry = c.entry;
                        entry.Car = c.car;
                        entries.Add(entry);
                    }
                    Entries = entries;
                    return Page();
                }
            }
            return RedirectToPage("./Create");
        }
    }
}
