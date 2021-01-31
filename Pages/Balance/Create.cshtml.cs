using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ParkingService.Pages.Balance
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ParkingService.Models.ParkingServiceContext _context;
        public int Id { get; set; }
        public Car Car { get; set; }
        [BindProperty]
        public Models.Balance Balance { get; set; }

        public CreateModel(ParkingService.Models.ParkingServiceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if(id!=null)
            {
                Id = Convert.ToInt32(id);
                Car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == Id);
                return Page();
            }
            return RedirectToPage("Index", new { id = Id });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if(Balance.Time <= DateTime.Now)
                {
                    Balance.Car = Car;
                    _context.Balances.Add(Balance);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index",new { id = Balance.CarId});
                }
                ModelState.AddModelError("", "Date and time can't be greater than today!");
            }
            return Page();
        }
    }
}
