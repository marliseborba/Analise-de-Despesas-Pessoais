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

        public KeyWord FindById(int id)
        {
            return _context.KeyWord
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public List<KeyWord> Insert(string keys)
        {   
            string[] news = keys.Split(',');
            List<KeyWord> keyWords = new List<KeyWord>();
            foreach (string s in news) 
            {
                KeyWord keyWord = new KeyWord();
                keyWord.Description = s;
                keyWords.Add(keyWord);
                _context.Add(keyWord);
                _context.SaveChanges();
            }
            return keyWords;
        }

        public KeyWord Update(KeyWord keyWord)
        {
            bool hasAny = _context.KeyWord.Any(x => x.Id == keyWord.Id);
            if (!hasAny)
            {
                //throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(keyWord);
                _context.SaveChanges();
                return keyWord;
            }
            catch (DbUpdateConcurrencyException e)
            {
                //throw new DbConcurrencyException(e.Message);
            }
            return null;
        }
    }
}
