using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.Models;

namespace ParkingService.Pages.Payment
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
                var list = _context.Entries.OrderByDescending(n => n.Id).ToList();
                List<Entry> entries = new List<Entry>();
                foreach(var item in list)
                {
                    Entry entry = item;
                    entry.Car = _context.Cars.FirstOrDefault(c => c.Id == item.CarId);

                    DateTime dateMax = _context.Prices.Where(p => p.DateTime <= DateTime.Now).Max(p => p.DateTime);
                    Price price = new Price();
                    if (dateMax != null)
                    {
                        price = _context.Prices.FirstOrDefault(p => p.DateTime == dateMax);
                    }

                    TimeSpan span = new TimeSpan();
                    if(entry.LeavingTime!=null)
                    {
                        DateTime date = Convert.ToDateTime(entry.LeavingTime);
                        span = date.Subtract(entry.EntryTime);
                    }
                    else
                    {
                        span = DateTime.Now.Subtract(entry.EntryTime);
                    }
                    int days = Convert.ToInt32(span.Days);
                    int hours = Convert.ToInt32(span.Hours);
                    entry.Payment = days * price.Day  + hours * price.Hour;
                    if(_context.Balances.Any(e=>e.EntryId==entry.Id))
                    {
                        var listBalance = _context.Balances.Where(e => e.EntryId == entry.Id).ToList();
                        decimal sum = 0;
                        foreach (var bal in listBalance)
                        {
                            sum += bal.Payment;
                        }
                        if (sum >= entry.Payment)
                        {
                            entry.IsPaid = true;
                        }
                        else
                        {
                            entry.IsPaid = false;
                            entry.Payment -= sum;
                        }
                    }
                    else
                    {
                        entry.IsPaid = false;
                    }
                    entries.Add(entry);
                }
                Entries = entries;
                return Page();

            }
            return RedirectToPage("/Journal/Create");
            
        }
    }
}
