using Expenses.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Models
{
    public class Movement : IComparable
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Value { get; set; }
        public string Identifier { get; set; }
        public Owner Owner { get; set; }
        public int? OwnerId { get; set; }
        public MovementType MovementType { get; set; }
        public Establishment Establishment { get; set; }
        public int? EstablishmentId { get; set; }
        public ICollection<Category>? Categories { get; set; } = new List<Category>();
        public SubCategory? SubCategory { get; set; }
        public int? SubCategoryId { get; set; }
        public string? Icon { get; set; } = "bi bi-coin";

        public Movement() 
        {
        }

        public Movement(int id, string description, DateTime date, double value, string identifier, Establishment establishment)
        {
            Id = id;
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
            Establishment = establishment;
        }

        public Movement(string description, DateTime date, double value, string identifier, MovementType type, Owner owner, Establishment establishment)
        {
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
            MovementType = type;
            Owner = owner;
            Establishment = establishment;
        }

        public Movement(string description, DateTime date, double value, string identifier, Owner owner, Establishment establishment)
        {
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
            Owner = owner;
            Establishment = establishment;
        }

        public Movement(string description, DateTime date, double value, string identifier, MovementType type)
        {
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
            MovementType = type;
        }
        public Movement(string description, DateTime date, double value, string identifier, Owner owner, List<Category> categories)
        {
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
            Owner = owner;
            Categories = categories;
        }

        public Movement(string description, DateTime date, double value, string identifier, Establishment establishment)
        {
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
            Establishment = establishment;
        }

        public Movement(string description, DateTime date, double value, string identifier)
        {
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
        }

        public Movement(int id, string description, DateTime date, double value, string identifier)
        {
            Id = id;
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
        }

        public int CompareTo(object? obj)
        {
            Movement other = (Movement)obj;
            return Identifier.CompareTo(other.Identifier);
        }

        public override bool Equals(object? obj)
        {
            Movement other = obj as Movement;
            return Identifier.Equals(other.Identifier);
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
    }
}
