using System.ComponentModel.DataAnnotations;

namespace Expenses.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public int Account { get; set; }

        public Owner()
        { 
        }

        public Owner(string name, string user, int account)
        {
            Name = name;
            User = user;
            Account = account;
        }
        public int CompareTo(object? obj)
        {
            Owner other = (Owner)obj;
            return Account.CompareTo(other.Account);
        }

        public override bool Equals(object? obj)
        {
            Owner other = obj as Owner;
            return Account.Equals(other.Account);
        }

        public override int GetHashCode()
        {
            return Account.GetHashCode();
        }
    }
}
