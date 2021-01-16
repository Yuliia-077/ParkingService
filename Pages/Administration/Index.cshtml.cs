using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.Models;

namespace ParkingService.Pages.Administration
{
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        public Price Price { get; set; }

        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public void OnGet()
        {
            DateTime date = _context.Prices.Where(p => p.DateTime <= DateTime.Now).Max(p=>p.DateTime);
            if (date != null)
            {
                Price = _context.Prices.FirstOrDefault(p => p.DateTime == date);
            }
        }
    }
}
