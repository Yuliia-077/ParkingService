using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.Models;

namespace ParkingService.Pages.Balance
{
    public class IndexModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        public List<Models.Balance> Balances { get; set; } 
        public int Id { get; set; }

        public IndexModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public IActionResult OnGet(int? id)
        {
            if(id != null)
            {
                Id = Convert.ToInt32(id);
                if (_context.Balances.Any(n => n.EntryId == Id))
                {
                    Balances = _context.Balances.Where(n => n.EntryId == id).ToList();
                    return Page();
                }
                return RedirectToPage("./Create", new { id = Id });
            }
            return RedirectToPage("/Payment/Index");
        }
    }
}
