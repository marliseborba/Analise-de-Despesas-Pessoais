using System.ComponentModel.DataAnnotations;

namespace Expenses.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string DebAccount { get; set; }
        public string CredAccount { get; set; }
        public IEnumerable<Movement> Movements { get; set; } = new List<Movement>();
        public string? Icon { get; set; }

        public Owner()
        { 
        }

        public Owner(string name, string user, string debAccount, string credAccount)
        {
            Name = name;
            User = user;
            DebAccount = debAccount;
            CredAccount = credAccount;
        }

        public IEnumerable<Movement> GetMovements()
        {
            return Movements;
        }  

        public double TotalExpenses()
        {
            return Movements.Where(x => x.Value < 0.0).Sum(x => x.Value) * -1;
        }

        public double TotalReceipts()
        {
            return Movements.Where(x => x.Value > 0.0).Sum(x => x.Value);
        }

        public int CompareTo(object? obj)
        {
            Owner other = (Owner)obj;
            return DebAccount.CompareTo(other.DebAccount);
        }

        public override bool Equals(object? obj)
        {
            Owner other = obj as Owner;
            return DebAccount.Equals(other.DebAccount);
        }

        public override int GetHashCode()
        {
            return DebAccount.GetHashCode();
        }
    }
}
