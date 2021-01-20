using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingService.Models;
using Microsoft.EntityFrameworkCore;

namespace ParkingService.ViewModels
{
    public class Counting
    {
        private readonly ParkingServiceContext _context;
        public Counting(ParkingServiceContext db)
        {
            _context = db;
        }

        public async Task<Entry> Debts(Entry entry)
        {
            DateTime dateMax = await _context.Prices.Where(p => p.DateTime <= DateTime.Now).MaxAsync(p => p.DateTime);
            Price price = new Price();
            if (dateMax != null)
            {
                price = await _context.Prices.FirstOrDefaultAsync(p => p.DateTime == dateMax);
            }

            TimeSpan span = new TimeSpan();
            if (entry.LeavingTime != null)
            {
                DateTime date = Convert.ToDateTime(entry.LeavingTime);
                span = date.Subtract(entry.EntryTime);
            }
            else
            {
                span = DateTime.Now.Subtract(entry.EntryTime);
            }
            int days = Convert.ToInt32(span.Days);
            int hours = Convert.ToInt32(span.Hours);
            entry.Payment = days * price.Day + hours * price.Hour;
            if (await _context.Balances.AnyAsync(e => e.EntryId == entry.Id))
            {
                var listBalance = await _context.Balances.Where(e => e.EntryId == entry.Id).ToListAsync();
                decimal sum = 0;
                foreach (var bal in listBalance)
                {
                    sum += bal.Payment;
                }
                if (sum >= entry.Payment)
                {
                    entry.IsPaid = true;
                }
                else
                {
                    entry.IsPaid = false;
                    entry.Payment -= sum;
                }
            }
            else
            {
                entry.IsPaid = false;
            }
            return entry;
        }

    }
}
