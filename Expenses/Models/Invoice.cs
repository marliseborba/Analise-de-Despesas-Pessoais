using System.Collections.Generic;

namespace Expenses.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime DtInitial { get; set; }
        public DateTime DtFinal { get; set; }
        public double Total { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
