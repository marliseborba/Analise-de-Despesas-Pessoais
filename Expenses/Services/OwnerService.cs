using Expenses.Data;
using Expenses.Models;

namespace Expenses.Services
{
    public class OwnerService
    {
        private ExpensesContext _context;

        public OwnerService(ExpensesContext context)
        {
            _context = context;
        }

        public List<Owner> GetOwners()
        {
            return _context.Owner.ToList();
        }
    }
}
