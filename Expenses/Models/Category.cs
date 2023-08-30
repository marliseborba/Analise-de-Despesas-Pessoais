using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public KeyWord? KeyWord { get; set; }
        [NotMapped]
        public List<int>? Keys { get; set; }
        public ICollection<KeyWord>? KeyWords { get; set; } = new List<KeyWord>();
        public ICollection<Movement>? Movements { get; set; } = new List<Movement>();
        public string? Icon { get; set; } = "bi bi-tags";

        public Category() { }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Category(string name)
        {
            Name = name;
        }

        public Category(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }

        public Category(string name, string icon, List<KeyWord> keyWords)
        {
            Name = name;
            Icon = icon;
            KeyWords = keyWords;
        }

        public Category(int id, string name, ICollection<Movement> movements) : this(id, name)
        {
            Movements = movements;
        }
    }
}
