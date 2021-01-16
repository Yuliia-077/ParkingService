using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.Models;

namespace ParkingService.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        public List<Models.Car> Cars { get; set; }
        
        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public void OnGet()
        {
            Cars = _context.Cars.OrderByDescending(p=>p.Id).ToList();
        }
    }
}
