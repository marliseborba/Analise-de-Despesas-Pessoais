using System.ComponentModel.DataAnnotations;

namespace Expenses.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movement>? Movements { get; set; } = new List<Movement>();
        public string? Icon { get; set; }

        public SubCategory() { }

        public SubCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public SubCategory(string name)
        {
            Name = name;
        }
    }
}
