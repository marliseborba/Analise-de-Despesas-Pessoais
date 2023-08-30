using System.ComponentModel.DataAnnotations;

namespace Expenses.Models
{
    public class KeyWord
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();

        public KeyWord()
        {
        }

        public KeyWord(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public KeyWord(int id, string description, ICollection<Category> categories)
        {
            Id = id;
            Description = description;
            Categories = categories;
        }

        public KeyWord(string description, ICollection<Category> categories)
        {
            Description = description;
            Categories = categories;
        }

        public KeyWord(string description)
        {
            Description = description;
        }
    }
}
