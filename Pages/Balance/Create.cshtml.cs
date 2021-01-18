using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkingService.Models;

namespace ParkingService.Pages.Balance
{
    public class CreateModel : PageModel
    {
        private readonly ParkingService.Models.ParkingServiceContext _context;
        public int Id { get; set; }
        [BindProperty]
        public Models.Balance Balance { get; set; }

        public CreateModel(ParkingService.Models.ParkingServiceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            if(id!=null)
            {
                Id = Convert.ToInt32(id);
                return Page();
            }
            return RedirectToPage("Index", new { id = Id });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Balances.Add(Balance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
