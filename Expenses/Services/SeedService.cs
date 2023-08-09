using Expenses.Models;

namespace Expenses.Services
{
    public class SeedService
    {
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
