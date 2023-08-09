using System.ComponentModel.DataAnnotations;

namespace Expenses.Models
{
    public class Movement
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Value { get; set; }
        public string Identifier { get; set; }
        public Establishment Establishment { get; set; }

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

        public Movement(int id, string description, DateTime date, double value, string identifier)
        {
            Id = id;
            Description = description;
            Date = date;
            Value = value;
            Identifier = identifier;
        }
    }
}
