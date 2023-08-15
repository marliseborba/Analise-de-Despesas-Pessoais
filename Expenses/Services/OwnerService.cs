using Expenses.Data;

namespace Expenses.Services
{
    public class OwnerService
    {
        private ExpensesContext _context;

        public OwnerService(ExpensesContext context)
        {
            _context = context;
        }

    }
}
