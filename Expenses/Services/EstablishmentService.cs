using Expenses.Data;
using Expenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Services
{
    public class EstablishmentService
    {
        private readonly ExpensesContext _context;

        public EstablishmentService(ExpensesContext context)
        {
            _context = context;
        }

        public List<Establishment> GetEstablishments() 
        {
            return _context.Establishment.Include(x => x.Category).ToList();
        }
    }
}
