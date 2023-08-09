namespace Expenses.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();

        public Category() { }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Category(int id, string name, ICollection<SubCategory> subCategories) : this(id, name)
        {
            SubCategories = subCategories;
        }

        public Category(int id, string name, ICollection<SubCategory> subCategories, ICollection<Establishment> establishments) : this(id, name, subCategories)
        {
            Establishments = establishments;
        }

        public Category(int id, string name, ICollection<Establishment> establishments) : this(id, name)
        {
            Establishments = establishments;
        }
    }
}
