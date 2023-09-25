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
            return _context.Category.Include(x => x.KeyWords).ToList();
        }

        public Category FindById(int id)
        {
            return _context.Category
                .Where(x => x.Id == id)
                .Include(x => x.KeyWords)
                .FirstOrDefault();
        }

        public Category Insert(Category category, List<int> keys)
        {
            List<KeyWord> news = _context.KeyWord.Where(x => keys.Contains(x.Id)).ToList();
            category.KeyWords = news;
            _context.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Update(Category category, List<int> keys)
        {
            bool hasAny = _context.Category.Any(x => x.Id == category.Id);
            if (!hasAny)
            {
                //throw new NotFoundException("Id not found");
            }
            try
            {
                category.KeyWords.Clear();
                _context.Update(category);
                _context.SaveChanges();

                List<KeyWord> olds = _context.KeyWord.Where(x => x.Categories.Contains(category)).Include(x => x.Categories).ToList();
                foreach (KeyWord k in olds)
                {
                    k.Establishments.Clear();
                    _context.Update(k);
                    _context.SaveChanges();
                }

                List<KeyWord> news = _context.KeyWord.Where(x => keys.Contains(x.Id)).ToList();
                category.KeyWords = news;
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

        public ICollection<KeyWord> LinkCategoryKeyWords(Category category, List<KeyWord> keyWords) 
        {
            List<KeyWord> olds = _context.KeyWord.Where(x => x.Id == category.Id).ToList();
            List<KeyWord> news = keyWords.Except(olds).ToList();
            foreach (KeyWord keyWord in news) 
            {
                category.KeyWords.Add(keyWord);
                _context.Update(category);
            }
            _context.SaveChanges();
            return news;
        }
    }
}
