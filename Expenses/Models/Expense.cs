namespace Expenses.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string Identifier { get; set; }
        public Invoice Invoice { get; set; }

    }
}
