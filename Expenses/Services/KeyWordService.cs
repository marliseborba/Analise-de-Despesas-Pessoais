using Expenses.Data;
using Expenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Services
{
    public class KeyWordService
    {
        private ExpensesContext _context;

        public KeyWordService(ExpensesContext context)
        {
            _context = context;
        }

        public List<KeyWord> GetKeyWords()
        {
            return _context.KeyWord.ToList();
        }
    }
}
