using Expenses.Models.Enums;
using Expenses.Models;
using System.Globalization;
using Expenses.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Expenses.Models.ViewModels;
using Aspose.Finance.Ofx;
using OfxSharp;
using Aspose.Finance.Xbrl.Dom;
using Aspose.Finance.Xbrl;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Expenses.Services
{
    public class MovementService
    {
        private readonly ExpensesContext _context;
        public IMemoryCache _memoryCache { get; set; }
        private readonly SeedingService _seedingService;
        private readonly OwnerService _ownerService;

        public MovementService(ExpensesContext context,
            IMemoryCache memoryCache,
            SeedingService seedingService,
            OwnerService ownerService)
        {
            _context = context;
            _memoryCache = memoryCache;
            _seedingService = seedingService;
            _ownerService = ownerService;
        }

        public void Insert(Movement movement)
        {
            _context.Add(movement);
            _context.SaveChanges();
        }

        public void Insert(List<Movement> movements)
        {
            _context.AddRange(movements);
            _context.SaveChanges();
        }

        public List<IGrouping<Owner, Movement>> GetMovements()
        {
            return _context.Movement
                    .Include(x => x.Establishment)
                    .Include(x => x.Owner)
                    .OrderBy(x => x.Date)
                    .GroupBy(x => x.Owner)
                    .ToList();
        }

        public List<Movement> GetMovementsNoGrouping()
        {
            return _context.Movement
                    .Include(x => x.Establishment)
                    .Include(x => x.Categories)
                    .Include(x => x.Owner)
                    .OrderBy(x => x.Date)
                    .ToList();
        }

        public List<IGrouping<Owner, Movement>> FindByDate(DateTime? minDate, DateTime? maxDate) 
        {
            var result = from obj in _context.Movement select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return result
                .Include(x => x.Owner)
                .Include(x => x.Establishment)
                .OrderBy(x => x.Date)
                .GroupBy(x => x.Owner)
                .ToList();
        }

        public List<Movement> FindByDateNoGrouping(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Movement select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return result
                .Include(x => x.Owner)
                .Include(x => x.Establishment)
                .OrderBy(x => x.Date)
                .ToList();
        }

        public List<Movement> SearchMovements(MovementViewModel viewModel)
        {
            var result = _context.Movement
                //.Where(x => x.Date >= viewModel.MinDate && x.Date <= viewModel.MaxDate)
                .Include(x => x.Owner)
                .Include(x => x.Establishment)
                .Include(x => x.Categories).ToList();

            result = result.Where(x => x.Date >= viewModel.MinDate && x.Date <= viewModel.MaxDate).ToList();

            if (viewModel.Owns.FirstOrDefault() != "Selecione...")
            {
                result = result.Where(x => viewModel.Owns.Contains(x.Owner.Name)).ToList();
            }

            if (viewModel.Estabs.FirstOrDefault() != "Selecione...")
            {
                result = result.Where(x => x.Establishment != null && viewModel.Estabs.Contains(x.Establishment.Name)).ToList();
            }
            
            if (viewModel.Cats.FirstOrDefault() != "Selecione...")
            {
                result = result.Where(x => x.Categories != null && x.Categories.Any(t => viewModel.Cats.Contains(t.Name))).ToList();
            }

            return result.ToList();
        }    

        public void PopulateViewModel(MovementViewModel viewModel)
        {
            foreach (var item in _context.Owner)
            {
                viewModel.Owners.Add(item);
            }

            foreach (var item in _context.Establishment)
            {
                viewModel.Establishments.Add(item);
            }

            foreach (var item in _context.Category)
            {
                viewModel.Categories.Add(item);
            }

            foreach (var item in _context.SubCategory)
            {
                viewModel.SubCategories.Add(item);
            }
        }

        public ICollection<Movement> UploadExtract(IList<IFormFile> files)
        {
            List<Movement> movements = new List<Movement>();
            _memoryCache.Set("movs", movements);

            try
            {
                foreach (var file in files)
                {
                    // Cria variaveis com path e nome
                    var fileName = file.FileName;
                    var filePath = Path.Combine(@"C:Temp\", fileName);

                    // Cria variavel com path do arquivo codificado + novo sufixo
                    var filePathEncoding = Path.Combine(@"C:Temp\", "Enc"+fileName);

                    // Cria a cópia do file
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fs);
                    }

                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            using (StreamWriter sw = File.AppendText(filePathEncoding))
                            {
                                while (!sr.EndOfStream)
                                {
                                    var line = sr.ReadLine();
                                    var l = line;

                                    if (line.Contains("UTF-8"))
                                    {
                                        l = line.Replace("UTF-8", "USASCII");
                                    }
                                    if (line.Contains("CHARSET:NONE"))
                                    {
                                        l = line.Replace("CHARSET:NONE", "CHARSET:1252");
                                    }

                                    sw.WriteLine(l);
                                }
                            }
                        }
                    }

                    var parser = new OFXDocumentParser();
                    var ofxDocument = parser.Import(new FileStream(filePathEncoding, FileMode.Open));

                    File.Delete(filePath);
                    File.Delete(filePathEncoding);

                    Owner own = new Owner();
                    string ownerId = ofxDocument.Account.AccountId;
                    string accType = ofxDocument.Account.AccountType.ToString();
                    if (accType == "BANK")
                    {
                        own = _context.Owner.Where(x => x.DebAccount.Equals(ownerId)).FirstOrDefault();
                    }
                    else if (accType == "CC")
                    {
                        own = _context.Owner.Where(x => x.CredAccount.Equals(ownerId)).FirstOrDefault();
                    }

                    foreach (var item in ofxDocument.Transactions)
                    {
                        DateTime date = item.Date;
                        double value = (double)item.Amount;
                        string identifier = item.TransactionId;
                        string description = item.Memo;
                        Establishment estab = new Establishment();
                        estab = CheckEstab(description);
                        Movement exp = new Movement(description, date, value, identifier, Enum.Parse<MovementType>(accType));
                        exp.OwnerId = own.Id;
                        if (estab != null && estab.Id > 0)
                        {
                            exp.EstablishmentId = estab.Id;
                        }
                        movements.Add(exp);
                    }
                }
            }
            catch (IOException e)
            {

            }

            return movements;
        }

        public ICollection<Movement> Upload(IList<IFormFile> files)
        {
            List<Movement> movements = new List<Movement>();
            _memoryCache.Set("movs", movements);
            try
            {
                foreach (var file in files)
                {
                    Owner own = CheckOwner(file.FileName);
                    using (StreamReader sr = new StreamReader(file.OpenReadStream()))
                    {
                        int count = 1;
                        double sum = 0;
                        string headerLine = sr.ReadLine();
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] attributes = line.Split(",");
                            DateTime date = DateTime.ParseExact(attributes[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            double value = double.Parse(attributes[1], CultureInfo.InvariantCulture);
                            string identifier = attributes[2];
                            string description = attributes[3];
                            Establishment estab = CheckEstab(description);
                            Movement exp = new Movement(description, date, value, identifier, own, estab);
                            exp.OwnerId = own.Id;
                            sum += value;
                            count++;
                            movements.Add(exp);
                        }
                    }
                }
            }
            catch (IOException e)
            {

            }

            return movements;
        }

        public ICollection<Movement> SaveInvoice()
        {
            ICollection<Movement> invoice = _memoryCache.Get("movs") as ICollection<Movement>;
            List<Movement> movements = _context.Movement.ToList();

            List<Movement> except = invoice.Except(movements).ToList();

            foreach (Movement mov in except)
            {
                //mov.Establishment = null;
                //mov.Owner = null;
            }

            _context.AddRange(except);
            _context.SaveChanges();
            return except;
        }

        public Establishment CheckEstab(string description)
        {
            List<KeyWord> keyWords = _context.KeyWord.ToList();
            ICollection<Establishment> estabs = _context.Establishment.ToList();
            Establishment est = new Establishment();
            foreach (var key in keyWords)
            {
                if (description != " "
                && description != ""
                && description != null
                && description.ToLower().Contains(key.Description.ToLower()))
                {
                    var e = _context.Establishment.Where(x => x.KeyWords.Any(k => k.Id == key.Id));
                    var query = from establishment in _context.Establishment
                                where establishment.KeyWords.Any(k => k.Id == key.Id)
                                select establishment;
                    est = query.FirstOrDefault();
                    //var c = _context.Category.Where(x => x.Id == key.Id).SelectMany(c => Categories).ToList();
                }
            }
            return est;
        }

        public Owner CheckOwner(string account)
        {
            ICollection<Owner> owns = _context.Owner.ToList();
            foreach (Owner owner in owns) 
            {
                if(account.Contains(owner.DebAccount.ToString()))
                {
                    return owner;
                }
            }
            return null;
        }

        public ICollection<Category> CheckCategory(string description)
        {
            List<KeyWord> keyWords = _context.KeyWord.ToList();
            List<Category> cats = new List<Category>();
            foreach (var key in keyWords)
            {
                if (key.Description != " "
                && key.Description != ""
                && key.Description != null
                && description.ToLower().Contains(key.Description.ToLower()))
                {
                    var query = from category in _context.Category
                                where category.KeyWords.Any(k => k.Id == key.Id)
                                select category;
                    //var c = _context.Category.Where(x => x.Id == key.Id).SelectMany(c => Categories).ToList();
                    foreach(var cat in query)
                    {
                        cats.Add(cat);
                    }
                }
            }
            return cats;
        }

        public ICollection<Movement> UpdateCategories()
        {
            var movements = _context.Movement.Include(x => x.Categories).ToList();
            var updated = new List<Movement>();
            foreach (Movement item in movements)
            {
                var cat = CheckCategory(item.Description);
                if (cat != null)
                {
                    ICollection<Category> old = item.Categories;
                    ICollection<Category> news = cat.Except(old).ToList();

                    foreach(var c in news)
                    {
                        item.Categories.Add(c);
                        _context.Update(item);
                        updated.Add(item);
                    }
                }
            }
            _context.SaveChanges();
            return updated;
        }

        public ICollection<Movement> UpdateEstablishments()
        {
            var movements = _context.Movement.ToList();
            var updated = new List<Movement>();
            foreach (Movement item in movements) 
            {
                Establishment estab = new Establishment();
                estab = CheckEstab(item.Description);
                if (estab != null && estab.Name != null) 
                {
                    item.EstablishmentId = estab.Id;

                    _context.Update(item);
                    updated.Add(item);
                    _context.SaveChanges();
                }
            }
            return updated;
        }
    }
}
