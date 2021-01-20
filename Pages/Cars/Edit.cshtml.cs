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

namespace ParkingService.Pages.Cars
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ParkingService.Models.ParkingServiceContext _context;
        [BindProperty]
        public Car Car { get; set; }

        public EditModel(ParkingService.Models.ParkingServiceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                Car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);
                if (Car != null)
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
                if (await _context.Cars.AnyAsync(x => x.PlateNumber == Car.PlateNumber && x.Id != Car.Id))
                {
                    ModelState.AddModelError("", "This plate number is used!");
                    return Page();
                }
                if (await _context.Cars.AnyAsync(x => x.PhoneNumber == Car.PhoneNumber && x.Id != Car.Id))
                {
                    ModelState.AddModelError("", "This phone number is used!");
                    return Page();
                }
                _context.Cars.Update(Car);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
