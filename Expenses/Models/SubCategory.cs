namespace Expenses.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();

        public SubCategory() { }

        public SubCategory(int id, string name, Category category)
        {
            Id = id;
            Name = name;
            Category = category;
        }
    }
}
