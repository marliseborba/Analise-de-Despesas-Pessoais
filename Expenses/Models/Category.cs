namespace Expenses.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

        public Category() { }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
