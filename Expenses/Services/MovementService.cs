using Expenses.Models.Enums;
using Expenses.Models;
using System.Globalization;
using Expenses.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IO;
using Microsoft.Extensions.Caching.Memory;

namespace Expenses.Services
{
    public class MovementService
    {
        private readonly ExpensesContext _context;
        public IMemoryCache _memoryCache { get; set; }
        private readonly SeedingService _seedingService;

        public MovementService(ExpensesContext context, IMemoryCache memoryCache, SeedingService seedingService)
        {
            _context = context;
            _memoryCache = memoryCache;
            _seedingService = seedingService;
        }

        public void Insert(Movement movement)
        {
            _context.Add(movement);
            _context.SaveChanges();
        }

        public void Insert(List<Movement> movements)
        {
            _context.AddRange(movements);
            _context.SaveChanges();
        }

        public List<IGrouping<Owner, Movement>> GetMovements()
        {
            return _context.Movement
                    .Include(x => x.Establishment)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Owner)
                    .OrderBy(x => x.Date)
                    .GroupBy(x => x.Owner)
                    .ToList();
        }

        public List<IGrouping<Owner, Movement>> FindByDate(DateTime? minDate, DateTime? maxDate) 
        {
            var result = from obj in _context.Movement select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return result
                .Include(x => x.Owner)
                .Include(x => x.Establishment)
                .OrderBy(x => x.Date)
                .GroupBy(x => x.Owner)
                .ToList();
        }

        public ICollection<Movement> Upload(IList<IFormFile> files)
        {
            List<Movement> movements = new List<Movement>();
            _memoryCache.Set("movs", movements);
            try
            {
                foreach (var file in files)
                {
                    Owner own = CheckOwner(file.FileName);
                    using (StreamReader sr = new StreamReader(file.OpenReadStream()))
                    {
                        int count = 1;
                        double sum = 0;
                        string headerLine = sr.ReadLine();
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] attributes = line.Split(",");
                            DateTime date = DateTime.ParseExact(attributes[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            double value = double.Parse(attributes[1], CultureInfo.InvariantCulture);
                            string identifier = attributes[2];
                            string description = attributes[3];
                            Establishment estab = CheckEstab(description);
                            Movement exp = new Movement(description, date, value, identifier, own, estab);
                            exp.OwnerId = own.Id;
                            sum += value;
                            count++;
                            movements.Add(exp);
                        }
                    }
                }
            }
            catch (IOException e)
            {

            }

            return movements;
        }

        public ICollection<Movement> SaveInvoice()
        {
            ICollection<Movement> invoice = _memoryCache.Get("movs") as ICollection<Movement>;
            List<Movement> movements = _context.Movement.ToList();

            List<Movement> except = invoice.Except(movements).ToList();

            foreach (Movement mov in except)
            {
                mov.Establishment = null;
                mov.Owner = null;
            }

            _context.AddRange(except);
            _context.SaveChanges();
            return except;
        }

        public Establishment CheckEstab(string description)
        {
            ICollection<Establishment> estabs = _context.Establishment.ToList();
            foreach (Establishment est in estabs)
            {
                if (est.Description != " "
                && est.Description != ""
                && est.Description != null
                && description.Contains(est.Description))
                {
                    return est;
                }
            }
            return null;
        }

        public Owner CheckOwner(string account)
        {
            ICollection<Owner> owns = _context.Owner.ToList();
            foreach (Owner owner in owns) 
            {
                if(account.Contains(owner.Account.ToString()))
                {
                    return owner;
                }
            }
            return null;
        }

        public ICollection<Movement> UpdateEstablishments()
        {
            var movements = _context.Movement.ToList();
            var updated = new List<Movement>();
            foreach (Movement item in movements) 
            {
                var estab = CheckEstab(item.Description);
                if (estab != null) 
                {
                    item.EstablishmentId = estab.Id;
                    _context.Update(item);
                    updated.Add(item);
                }
            }
            _context.SaveChanges();
            return updated;
        }
    }
}
