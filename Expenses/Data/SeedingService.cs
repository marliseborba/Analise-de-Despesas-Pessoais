using Expenses.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expenses.Data
{
    public class SeedingService
    {
        private ExpensesContext _context;

        public SeedingService(ExpensesContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            Owner own1 = new Owner("Marlise", "marlise", 87348844);
            Owner own2 = new Owner("Monique", "monique", 56442900);

            Category cat1 = new Category("Mercado");
            Category cat2 = new Category("Farmácia");
            Category cat3 = new Category("Bichos");
            Category cat4 = new Category("Aluguel");
            Category cat5 = new Category("Luz");
            Category cat6 = new Category("Água");
            Category cat7 = new Category("Internet");
            Category cat8 = new Category("Transportes");
            Category cat9 = new Category("Lanches");
            
            Establishment estab1 = new Establishment("Soberano", " Supermercado Soberano ", cat1);
            Establishment estab2 = new Establishment("Nacional", " ", cat1);
            Establishment estab3 = new Establishment("Zaffari", " ", cat1);
            Establishment estab4 = new Establishment("Kuchak", " ", cat1);
            Establishment estab5 = new Establishment("Yes Burguer", "FLAVIO F SOARES ", cat9);
            Establishment estab6 = new Establishment("Vero", "VERO S A", cat7);
            Establishment estab7 = new Establishment("Demei", "DEMEI", cat5);
            Establishment estab8 = new Establishment("Corsan", "CORSAN", cat6);

            Movement mov1 = new Movement(
                "Transferência Recebida - Monique Zaioncz Roloff - •••.085.859-•• - NU PAGAMENTOS - IP (0260) Agência: 1 Conta: 5644290-0",
                new DateTime(2023, 07, 06),
                100.0,
                "64a6fbf5-a46a-42f7-8e4e-3a358a36069b"
                );

            if (!_context.Owner.Any())
            {
                _context.Owner.AddRange(own1, own2);
            }

            if (!_context.Category.Any())
            {
                _context.Category.AddRange(cat1, cat2, cat3, cat4, cat5, cat6, cat7, cat8, cat9);
            }

            if (!_context.Establishment.Any())
            {
                _context.Establishment.AddRange(estab1, estab2, estab3, estab4, estab5, estab6, estab7, estab8);
            }

            if (!_context.Movement.Any())
            {
                _context.Movement.Add(mov1);
            }

            _context.SaveChanges();
        }


        public ICollection<Category> CategorySeeding()
        {
            Category cat1 = new Category(0, "Mercado");
            Category cat2 = new Category(1, "Farmácia");
            Category cat3 = new Category(2, "Bichos");
            Category cat4 = new Category(3, "Aluguel");
            Category cat5 = new Category(4, "Luz");
            Category cat6 = new Category(5, "Água");
            Category cat7 = new Category(6, "Internet");
            Category cat8 = new Category(7, "Transportes");
            Category cat9 = new Category(8, "Lanches");
            //Category cat = new Category(, "");
            //Category cat = new Category(, "");
            //Category cat = new Category(, "");

            ICollection<Category> categories = new List<Category>();
            categories.Add(cat1);
            categories.Add(cat2);
            categories.Add(cat3);
            categories.Add(cat4);
            categories.Add(cat5);
            categories.Add(cat6);
            categories.Add(cat7);
            categories.Add(cat8);
            categories.Add(cat9);

            return categories;
        }

        public ICollection<SubCategory> SubCategorySeeding()
        {
            //SubCategory sub1 = new SubCategory(0, "L", )    

            ICollection<SubCategory> subCategories = new List<SubCategory>();
            return subCategories;
        }

        public ICollection<Establishment> EstablishmentSeeding()
        {
            Establishment estab1 = new Establishment(0, "Soberano", " Supermercado Soberano ", new Category(0, "Mercado"));
            Establishment estab2 = new Establishment(1, "Nacional", "", new Category(0, "Mercado"));
            Establishment estab3 = new Establishment(2, "Zaffari", "", new Category(0, "Mercado"));
            Establishment estab4 = new Establishment(3, "Kuchak", "", new Category(0, "Mercado"));
            Establishment estab5 = new Establishment(4, "Yes Burguer", "FLAVIO F SOARES ", new Category(8, "Lanches"));
            Establishment estab6 = new Establishment(5, "Vero", "VERO S A", new Category(6, "Internet"));
            Establishment estab7 = new Establishment(6, "Demei", "DEMEI", new Category(4, "Luz"));
            Establishment estab8 = new Establishment(7, "Corsan", "CORSAN", new Category(5, "Água"));
            //Establishment estab9 = new Establishment(8, "", "");
            //Establishment estab6 = new Establishment(, "", "");
            //Establishment estab6 = new Establishment(, "", "");
            //Establishment estab6 = new Establishment(, "", "");

            ICollection<Establishment> establishments = new List<Establishment>();
            establishments.Add(estab1);
            establishments.Add(estab2);
            establishments.Add(estab3);
            establishments.Add(estab4);
            establishments.Add(estab5);
            establishments.Add(estab6);
            establishments.Add(estab7);
            establishments.Add(estab8);
            return establishments;
        }
    }
}
