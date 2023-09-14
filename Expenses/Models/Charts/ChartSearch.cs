namespace Expenses.Models.Charts
{
    public class ChartSearch
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public Owner Owner { get; set; }
        public int? OwnerId { get; set; }
        public Establishment? Establishment { get; set; }
        public int? EstablishmentId { get; set; }
    }
}
