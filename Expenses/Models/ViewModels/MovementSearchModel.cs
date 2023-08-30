namespace Expenses.Models.ViewModels
{
    public class MovementSearchModel
    {
        public DateTime minDate { get; set; }
        public DateTime maxDate { get; set; }
        public Owner Owner { get; set; }
        public int? OwnerId { get; set; }
        public Establishment? Establishment { get; set; }
        public int? EstablishmentId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
    }
}
