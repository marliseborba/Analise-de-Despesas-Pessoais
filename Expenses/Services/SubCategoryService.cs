using Expenses.Data;
using Expenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Services
{
    public class SubCategoryService
    {
        private readonly ExpensesContext _context;

        public SubCategoryService(ExpensesContext context)
        {
            _context = context;
        }
        public List<SubCategory> GetSubCategories()
        {
            return _context.SubCategory.ToList();
        }

        public void Insert(SubCategory subCategory)
        {
            _context.Add(subCategory);
            _context.SaveChanges();
        }

        public SubCategory Update(SubCategory subCategory)
        {
            bool hasAny = _context.SubCategory.Any(x => x.Id == subCategory.Id);
            if (!hasAny)
            {
                //throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(subCategory);
                _context.SaveChanges();
                return subCategory;
            }
            catch (DbUpdateConcurrencyException e)
            {
                //throw new DbConcurrencyException(e.Message);
            }
            return null;
        }

        public SubCategory FindById(int id)
        {
            return _context.SubCategory
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}
