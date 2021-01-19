using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingService.Models;

namespace ParkingService.Pages.Balance
{
    public class EditModel : PageModel
    {
        private readonly ParkingService.Models.ParkingServiceContext _context;

        public EditModel(ParkingService.Models.ParkingServiceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Balance Balance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Balance = await _context.Balances
                .Include(b => b.Entry).FirstOrDefaultAsync(m => m.Id == id);

            if (Balance == null)
            {
                return NotFound();
            }
           ViewData["EntryId"] = new SelectList(_context.Entries, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Balance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BalanceExists(Balance.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BalanceExists(int id)
        {
            return _context.Balances.Any(e => e.Id == id);
        }
    }
}
