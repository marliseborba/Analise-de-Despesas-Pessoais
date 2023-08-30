using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Establishment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<KeyWord>? KeyWords { get; set; } = new List<KeyWord>();
        public ICollection<Movement>? Movements { get; set; } = new List<Movement>();
        public string? Icon { get; set; } = "bi bi-shop-window";

        public Establishment()
        {
        }

        public Establishment(int id, string name, List<KeyWord> keyWords)
        {
            Id = id;
            Name = name;
            KeyWords = keyWords;
        }

        public Establishment(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Establishment(string name)
        {
            Name = name;
        }

        public Establishment(string name, List<KeyWord> keyWords)
        {
            Name = name;
            KeyWords = keyWords;
        }
    }
}
