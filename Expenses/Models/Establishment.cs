namespace Expenses.Models
{
    public class Establishment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
