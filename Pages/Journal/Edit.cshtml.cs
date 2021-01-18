using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingService.Models;

namespace ParkingService.Pages.Journal
{
    public class EditModel : PageModel
    {
        private readonly ParkingService.Models.ParkingServiceContext _context;
        public List<Car> Cars { get; set; }
        [BindProperty]
        public Entry Entry { get; set; }

        public EditModel(ParkingService.Models.ParkingServiceContext context)
        {
            _context = context;
        }

        public List<Car> CarsList(Car x)
        {
            var listCars = from cars in _context.Cars
                           where cars.Id!=x.Id
                           select cars;
            List<Car> Cars = new List<Car>();
            Cars.Add(x);
            if (listCars != null)
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entry = await _context.Entries
                .Include(e => e.Car).FirstOrDefaultAsync(m => m.Id == id);
            Car car = await _context.Cars.FirstOrDefaultAsync(s => s.Id == Entry.CarId);

            if (Entry == null)
            {
                return NotFound();
            }

            Cars = CarsList(car);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(Entry.Id))
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

        private bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.Id == id);
        }
    }
}
