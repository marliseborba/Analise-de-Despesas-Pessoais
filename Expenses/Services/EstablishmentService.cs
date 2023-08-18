using Expenses.Data;
using Expenses.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Expenses.Services
{
    public class EstablishmentService
    {
        private readonly ExpensesContext _context;
        private readonly MovementService _movementService;

        public EstablishmentService(ExpensesContext context, MovementService movementService)
        {
            _context = context;
            _movementService = movementService;
        }

        public List<Establishment> GetEstablishments() 
        {
            return _context.Establishment
                .Include(x => x.Category)
                .Include(x => x.SubCategory)
                .ToList();
        }

        public Establishment FindById(int id) 
        {
            return _context.Establishment
                .Include(x => x.Category)
                .Include(x => x.SubCategory)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void Insert(Establishment establishment) 
        {
            _context.Add(establishment);
            _context.SaveChanges();
        }

        public Establishment Update(Establishment establishment)
        {
            bool hasAny = _context.Establishment.Any(x => x.Id == establishment.Id);
            if (!hasAny)
            {
                //throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(establishment);
                _context.SaveChanges();
                return establishment;
            }
            catch (DbUpdateConcurrencyException e)
            {
                //throw new DbConcurrencyException(e.Message);
            }
            return null;
        }
    }
}
