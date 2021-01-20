using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkingService.Models;
using Microsoft.AspNetCore.Authorization;

namespace ParkingService.Pages.Journal
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ParkingService.Models.ParkingServiceContext _context;
        public List<Car> Cars { get; set; }
        [BindProperty]
        public Entry Entry { get; set; }

        public CreateModel(ParkingService.Models.ParkingServiceContext context)
        {
            _context = context;
        }

        public List<Car> CarsList()
        {
            var listCars = from cars in _context.Cars
                                 select cars;
            List<Car> Cars = new List<Car>();
            if (listCars!=null)
            {
                foreach (var c in listCars)
                {
                    Car car = new Car();
                    car = c;
                    Cars.Add(car);
                }
            }
            return Cars;
        }

        public void OnGet()
        {
            Cars = CarsList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            { 
                _context.Entries.Add(Entry);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
