using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingService.Models;
using Microsoft.AspNetCore.Authorization;

namespace ParkingService.Pages.Balance
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ParkingService.Models.ParkingServiceContext _context;
        [BindProperty]
        public Models.Balance Balance { get; set; }

        public EditModel(ParkingService.Models.ParkingServiceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id!=null)
            {
                Balance = await _context.Balances.FirstOrDefaultAsync(m => m.Id == id);
                if(Balance!=null)
                {
                    return Page();
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Balance.Time <= DateTime.Now)
                {
                    _context.Balances.Update(Balance);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                ModelState.AddModelError("", "Date and time can't be greater than today!");
            }
            return Page();
        }
    }
}
