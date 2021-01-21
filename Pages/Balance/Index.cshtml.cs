using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkingService.Models;
using Microsoft.AspNetCore.Authorization;

namespace ParkingService.Pages.Balance
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        public List<Models.Balance> Balances { get; set; } 
        public int Id { get; set; }

        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id != null)
            {
                Id = Convert.ToInt32(id);
                /*if (await _context.Balances.AnyAsync(n => n.EntryId == Id))
                {
                    Balances = await _context.Balances.Where(n => n.EntryId == id).ToListAsync();
                    return Page();
                }*/
                return RedirectToPage("./Create", new { id = Id });
            }
            return RedirectToPage("/Payment/Index");
        }
    }
}
