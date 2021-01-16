using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkingService.Models;

namespace ParkingService.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly ParkingService.Models.ParkingServiceContext _context;
        [BindProperty]
        public ParkingService.Models.Car Car { get; set; }

        public CreateModel(ParkingService.Models.ParkingServiceContext context)
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

            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
