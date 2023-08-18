namespace Expenses.Models.ViewModels
{
    public class EstablishmentViewModel
    {
        public Establishment Establishment { get; set; }
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
