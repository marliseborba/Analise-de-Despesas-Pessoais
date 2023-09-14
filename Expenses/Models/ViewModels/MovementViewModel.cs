using System.ComponentModel.DataAnnotations;

namespace Expenses.Models.ViewModels
{
    public class MovementViewModel
    {
        public Movement Movement { get; set; }
        public ICollection<Movement> Movements { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public DateTime MinDate { get; set; } = new DateTime(2021, 1, 1);
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public DateTime MaxDate { get; set; } = DateTime.Now;
        public Establishment Establishment { get; set; } = new Establishment();
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();
        public List<string> Estabs { get; set; } = new List<string>();
        public Category Category { get; set; } = new Category();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public List<string> Cats { get; set; } = new List<string>();
        public SubCategory SubCategory { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public Owner Owner { get; set; }
        public ICollection<Owner> Owners { get; set; } = new List<Owner>();
        public List<string> Owns { get; set; } = new List<string>();
        public MovementViewModel()
        {
        }
    }
}
