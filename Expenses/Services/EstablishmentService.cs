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
            return _context
                .Establishment
                .Include(x => x.KeyWords)
                .ToList();
        }

        public Establishment FindById(int id) 
        {
            return _context.Establishment
                .Where(x => x.Id == id)
                .Include(x => x.KeyWords)
                .FirstOrDefault();
        }

        public void Insert(Establishment establishment, List<int> keys) 
        {
            List<KeyWord> news = _context.KeyWord.Where(x => keys.Contains(x.Id)).ToList();
            establishment.KeyWords = news;
            _context.Add(establishment);
            _context.SaveChanges();
        }

        public Establishment Update(Establishment establishment, List<int> keys)
        {
            bool hasAny = _context.Establishment.Any(x => x.Id == establishment.Id);
            if (!hasAny)
            {
                //throw new NotFoundException("Id not found");
            }
            try
            {
                establishment.KeyWords.Clear();
                _context.Update(establishment);
                _context.SaveChanges();

                List<KeyWord> olds = _context.KeyWord.Where(x => x.Establishments.Contains(establishment)).Include(x => x.Establishments).ToList();
                foreach(KeyWord k in olds)
                {
                    k.Establishments.Clear();
                    _context.Update(k);
                    _context.SaveChanges();
                }

                List<KeyWord> news = _context.KeyWord.Where(x => keys.Contains(x.Id)).ToList();
                establishment.KeyWords = news;
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

        public ICollection<KeyWord> LinkEstablishmentKeyWords(Establishment establishment, List<KeyWord> keyWords)
        {
            
            List<KeyWord> olds = establishment.KeyWords.ToList();
            List<KeyWord> news = keyWords.Except(olds).ToList();
            foreach (KeyWord keyWord in news)
            {
                establishment.KeyWords.Add(keyWord);
                _context.Update(establishment);
            }
            _context.SaveChanges();
            return news;
        }
    }
}
