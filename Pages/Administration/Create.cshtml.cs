using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkingService.Models;
using Microsoft.AspNetCore.Authorization;

namespace ParkingService.Pages.Administration
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        [BindProperty]
        public Price Price { get; set; }

        public CreateModel(ParkingServiceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Prices.Add(Price);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
