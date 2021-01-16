using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ParkingService.Models;

namespace ParkingService.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;

        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public void OnGet()
        {
         
        }
    }
}
