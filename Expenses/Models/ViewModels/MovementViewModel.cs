using System.ComponentModel.DataAnnotations;

namespace Expenses.Models.ViewModels
{
    public class MovementViewModel
    {
        public Movement Movement { get; set; }
        public ICollection<Movement> Movements { get; set; } = new List<Movement>();
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public DateTime MinDate { get; set; } = new DateTime(2021, 1, 1);
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public DateTime MaxDate { get; set; } = DateTime.Now;
        public Establishment Establishment { get; set; }
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();
        public Category Category { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public SubCategory SubCategory { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public Owner Owner { get; set; }
        public ICollection<Owner> Owners { get; set; } = new List<Owner>();

        public MovementViewModel()
        {
        }
    }
}
