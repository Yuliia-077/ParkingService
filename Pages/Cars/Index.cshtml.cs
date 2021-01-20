using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

        public ContentResult OnGetDelete(int id)
        {
            Car car = _context.Cars.FirstOrDefault(p => p.Id == id);
            if (car != null)
            {
                List<Entry> entries = _context.Entries.Where(k=>k.CarId == car.Id).ToList();
                decimal sumDebts = 0;
                foreach(Entry entry in entries)
                {
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
                            entry.Payment = 0;
                        }
                        else
                        {
                            entry.Payment -= sum;
                        }
                    }
                    sumDebts += entry.Payment;
                }
                if(sumDebts == 0)
                {
                    _context.Cars.Remove(car);
                    _context.SaveChanges();
                    Message = "Information deleted";
                }
                else
                {
                    Message = "Debt in the amount of " + Convert.ToString(sumDebts);
                }
            }
            return Content(Message);
        }
    }
}
