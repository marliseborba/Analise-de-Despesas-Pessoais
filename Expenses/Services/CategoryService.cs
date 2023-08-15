using Expenses.Data;
using Expenses.Models;

namespace Expenses.Services
{
    public class CategoryService
    {
        private readonly ExpensesContext _context;

        public CategoryService(ExpensesContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories() 
        {
            return _context.Category.ToList();
        }
    }
}
