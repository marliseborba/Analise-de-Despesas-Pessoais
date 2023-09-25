using Expenses.Models;
using Expenses.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expenses.Data
{
    public class SeedingService
    {
        private ExpensesContext _context;
        private CategoryService _categoryService;

        public SeedingService(ExpensesContext context, CategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        public void Seed()
        {
            Owner own1 = new Owner("Marlise", "marlise", "8734884-4", "575886f1-0801-45a2-9d71-f7c805a58d51");
            Owner own2 = new Owner("Monique", "monique", "5644290-0", "");

            KeyWord k1 = new KeyWord("Corsan");
            KeyWord k2 = new KeyWord("Demei");
            KeyWord k3 = new KeyWord("Taba ");
            KeyWord k4 = new KeyWord("Mercado ");
            KeyWord k5 = new KeyWord("Supermercado ");

            List<KeyWord> keysW1 = new List<KeyWord>() { new KeyWord("Agropecuaria"), new("Veterinaria"), new KeyWord("Petlove") };
            List<KeyWord> keysW2 = new List<KeyWord>() { new KeyWord("Aiqfome"), new KeyWord("FLAVIO F SOARES"), new KeyWord("Sorveteria") };
            List<KeyWord> keysW3 = new List<KeyWord>() { k4, k5 };
            List<KeyWord> keysW4 = new List<KeyWord>() { new KeyWord("Cinema"), new KeyWord("Pub") };
            List<KeyWord> keysW5 = new List<KeyWord>() { new KeyWord("Farmacia") };
            List<KeyWord> keysW6 = new List<KeyWord>() { new KeyWord("Garupa") };
            List<KeyWord> keysW7 = new List<KeyWord>() { k1, k2, k3, k4, k5 };
            List<KeyWord> keysW8 = new List<KeyWord>() { k2 };
            List<KeyWord> keysW9 = new List<KeyWord>() { k1 };
            List<KeyWord> keysW10 = new List<KeyWord>() { k3 };

            Category cat1 = new Category("Casa", "bi bi-house-heart", keysW7);
            Category cat2 = new Category("Farmácia", "bi bi-capsule", keysW5);
            Category cat3 = new Category("Bichos", "fa-solid fa-paw", keysW1);
            Category cat4 = new Category("Transportes", "bi bi-taxi-front-fill", keysW6);
            Category cat5 = new Category("Lanches", "fa solid fa-burger", keysW2);
            Category cat6 = new Category("Lazer", "bi bi-controller");
            Category cat7 = new Category("Mercado", "bi bi-cart4", keysW3);
            Category cat8 = new Category("Água", "bi bi-droplet-half", keysW9);
            Category cat9 = new Category("Luz", "bi bi-lightning-fill", keysW8);
            Category cat10 = new Category("Aluguel", "bi bi-house-check", keysW10);
            Category cat11 = new Category("Poupança", "bi bi-piggy-bank");

            SubCategory sub1 = new SubCategory("Aluguel");
            SubCategory sub2 = new SubCategory("Internet");
            SubCategory sub3 = new SubCategory("Luz");
            //SubCategory sub4 = new SubCategory("Água", "bi bi-droplet-hlf");
            SubCategory sub5 = new SubCategory("Ração");
            SubCategory sub6 = new SubCategory("Veterinário");
            SubCategory sub7 = new SubCategory("Pub");
            SubCategory sub8 = new SubCategory("Cinema");
            SubCategory sub9 = new SubCategory("Mercado");

            /*
            Establishment estab1 = new Establishment("Soberano", "Supermercado Soberano");
            Establishment estab2 = new Establishment("Nacional", "Nacional");
            Establishment estab3 = new Establishment("Zaffari", "Nacional ");
            Establishment estab4 = new Establishment("Kuchak", "Kuchak");
            Establishment estab5 = new Establishment("Yes Burguer", "FLAVIO F SOARES");
            Establishment estab6 = new Establishment("Vero", "VERO S A");
            Establishment estab7 = new Establishment("Demei", "DEMEI");
            Establishment estab8 = new Establishment("Corsan", "CORSAN");
            */
            List<Category> categories = new List<Category> { cat1, cat7 };

            Movement mov1 = new Movement(
                "Compra no débito - Supermercado Soberano",
                new DateTime(2023, 08, 04),
                -42.33,
                "64cd5eeb-4660-4c23-a591-946d8fed1bc6",
                own1,
                categories
                );

            if (!_context.Owner.Any())
            {
                _context.Owner.AddRange(own1, own2);
            }

            if (!_context.KeyWord.Any())
            {
                _context.KeyWord.AddRange(k1, k2, k3);
            }

            if (!_context.Category.Any())
            {
                _context.Category.AddRange(cat1, cat2, cat3, cat4, cat5, cat6, cat7, cat8, cat9, cat10, cat11);
            }

            if (!_context.SubCategory.Any())
            {
                _context.SubCategory.AddRange(sub1, sub2, sub3, sub5, sub6, sub7, sub8, sub9);
            }

            if (!_context.Establishment.Any())
            {
                //_context.Establishment.AddRange(estab1, estab2, estab3, estab4, estab5, estab6, estab7, estab8);
            }

            if (!_context.Movement.Any())
            {
                _context.Movement.Add(mov1);
            }
            _context.SaveChanges();
        }

        public void SeedInitial()
        {
            if (!_context.Owner.Any())
            {
                SeedOwner();
            }
            if (!_context.KeyWord.Any())
            {
                UploadKeyWord();
            }
            if (!_context.Category.Any())
            {
                UploadCategory();
            }
            if (!_context.Establishment.Any())
            {
                UploadEstablishment();
            }
            _context.SaveChanges();
        }

        public void SeedFake()
        {
            if (!_context.Owner.Any())
            {
                SeedFakeOwner();
            }
            if (!_context.KeyWord.Any())
            {
                UploadFakeKeyWord();
            }
            if (!_context.Category.Any())
            {
                UploadFakeCategory();
            }
            if (!_context.Establishment.Any())
            {
                UploadFakeEstablishment();
            }
            _context.SaveChanges();
        }

        public void SeedOwner()
        {
            Owner own1 = new Owner("Marlise", "marlise", "8734884-4", "575886f1-0801-45a2-9d71-f7c805a58d51");
            Owner own2 = new Owner("Monique", "monique", "5644290-0", "");
            _context.Add(own1);
            _context.Add(own2);
            _context.SaveChanges();
        }
        public void SeedFakeOwner()
        {
            Owner own1 = new Owner("Fulana", "fulana", "conta-deb-1", "conta-cred-1");
            Owner own2 = new Owner("Ciclana", "ciclana", "conta-deb-2", "conta-cred-2");
            _context.Add(own1);
            _context.Add(own2);
            _context.SaveChanges();
        }

        public ICollection<KeyWord> UploadKeyWord()
        {
            if (!_context.KeyWord.Any())
            {
                string path = @"G:\Meu Drive\Dev\Expenses\Expenses\Seed\KeyWord.csv";
                List<KeyWord> keyWords = new List<KeyWord>();
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        KeyWord keyWord = new KeyWord(line);
                        //string[] line = sr.ReadLine().Split(",");
                        //KeyWord keyWord = new KeyWord(int.Parse(line[0]), line[1]);
                        keyWords.Add(keyWord);
                        _context.Add(keyWord);
                    }
                }
                _context.SaveChanges();
                return keyWords;
            }
            return null;
        }

        public ICollection<KeyWord> UploadFakeKeyWord()
        {
            if (!_context.KeyWord.Any())
            {
                string path = @"G:\Meu Drive\Dev\Expenses\Expenses\Seed\Fake\FakeKeyWord.csv";
                List<KeyWord> keyWords = new List<KeyWord>();
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        KeyWord keyWord = new KeyWord(line);
                        //string[] line = sr.ReadLine().Split(",");
                        //KeyWord keyWord = new KeyWord(int.Parse(line[0]), line[1]);
                        keyWords.Add(keyWord);
                        _context.Add(keyWord);
                    }
                }
                _context.SaveChanges();
                return keyWords;
            }
            return null;
        }

        public ICollection<Category> UploadCategory()
        {
            if (!_context.Category.Any())
            {
                string path = @"G:\Meu Drive\Dev\Expenses\Expenses\Seed\Category.csv";
                List<Category> categories = new List<Category>();
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Category category = new Category(line);
                        //string[] line = sr.ReadLine().Split(",");
                        //Category category = new Category(int.Parse(line[0]), line[1]);
                        categories.Add(category);
                        _context.Add(category);
                    }
                }
                _context.SaveChanges();
                return categories;
            }

            _context.SaveChanges();

            return null;
        }

        public ICollection<Category> UploadFakeCategory()
        {
            if (!_context.Category.Any())
            {
                string path = @"G:\Meu Drive\Dev\Expenses\Expenses\Seed\Fake\FakeCategory.csv";
                List<Category> categories = new List<Category>();
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Category category = new Category(line);
                        //string[] line = sr.ReadLine().Split(",");
                        //Category category = new Category(int.Parse(line[0]), line[1]);
                        categories.Add(category);
                        _context.Add(category);
                    }
                }
                _context.SaveChanges();
                return categories;
            }

            _context.SaveChanges();

            return null;
        }

        public ICollection<Establishment> UploadEstablishment()
        {
            if (!_context.Establishment.Any())
            {
                string path = @"G:\Meu Drive\Dev\Expenses\Expenses\Seed\Establishment.csv";
                List<Establishment> establishments = new List<Establishment>();
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Establishment establishment = new Establishment(line);
                        //string[] line = sr.ReadLine().Split(",");
                        //Establishment establishment = new Establishment(int.Parse(line[0]), line[1]);
                        establishments.Add(establishment);
                        _context.Add(establishment);
                    }
                }
                _context.SaveChanges();
                return establishments;
            }
            return null;
        }

        public ICollection<Establishment> UploadFakeEstablishment()
        {
            if (!_context.Establishment.Any())
            {
                string path = @"G:\Meu Drive\Dev\Expenses\Expenses\Seed\Fake\FakeEstablishment.csv";
                List<Establishment> establishments = new List<Establishment>();
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Establishment establishment = new Establishment(line);
                        //string[] line = sr.ReadLine().Split(",");
                        //Establishment establishment = new Establishment(int.Parse(line[0]), line[1]);
                        establishments.Add(establishment);
                        _context.Add(establishment);
                    }
                }
                _context.SaveChanges();
                return establishments;
            }
            return null;
        }
    }
}
