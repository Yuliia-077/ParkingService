using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkingService.Models;
using ParkingService.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ParkingService.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ParkingServiceContext _context;
        [BindProperty]
        public ViewModels.LoginModel Login { get; set; }

        public LoginModel(ParkingServiceContext db)
        {
            _context = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Login.Email && u.Password == Login.Password);
                if(user!=null)
                {
                    await Authenticate(user);
                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError("", "Login and(or) password are incorrect!");
            }
            return Page();
        }

        public async Task<IActionResult> OnGetLogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("Login");
        }

        private async Task Authenticate(User user)
        {
            // ������� ���� claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };
            // ������� ������ ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // ��������� ������������������ ����
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
