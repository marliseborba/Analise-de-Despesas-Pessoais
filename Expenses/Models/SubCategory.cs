namespace Expenses.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
