using Expenses.Data;
using Expenses.Models;
using Microsoft.EntityFrameworkCore;

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

        public void Insert(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public Category Update(Category category)
        {
            bool hasAny = _context.Category.Any(x => x.Id == category.Id);
            if (!hasAny)
            {
                //throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(category);
                _context.SaveChanges();
                return category;
            }
            catch (DbUpdateConcurrencyException e)
            {
                //throw new DbConcurrencyException(e.Message);
            }
            return null;
        }

        public Category FindById(int id)
        {
            return _context.Category
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}
