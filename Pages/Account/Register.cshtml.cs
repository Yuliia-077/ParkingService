using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingService.ViewModels;
using Microsoft.EntityFrameworkCore;
using ParkingService.Models;
using Microsoft.AspNetCore.Authorization;

namespace ParkingService.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        [BindProperty]
        public ViewModels.RegisterModel Register { get; set; }

        public RegisterModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Register.Email);
                if (user == null)
                {
                    _context.Users.Add(new User { Name = Register.Name, Email = Register.Email, Password = Register.Password});
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Index");
                }
                else
                    ModelState.AddModelError("", "This Email is used!");
            }
            return Page();

        }
    }
}
