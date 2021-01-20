using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ParkingService.Pages.Administration
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        public Price Price { get; set; }

        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if(await _context.Prices.AnyAsync())
            {
                DateTime date = await _context.Prices.Where(p => p.DateTime <= DateTime.Now).MaxAsync(p => p.DateTime);
                if (date != null)
                {
                    Price = await _context.Prices.FirstOrDefaultAsync(p => p.DateTime == date);
                    return Page();
                }
            }
            return RedirectToPage("./Create");
        }
    }
}
