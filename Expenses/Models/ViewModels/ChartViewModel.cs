using Expenses.Services;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Expenses.Models.ViewModels
{
    public class ChartViewModel
    {
        public string ChartJ { get; set; }
        [DataMember(Name = "chart")]
        public Expenses.Models.Charts.Chart Chart { get; set; }
        [DataMember(Name = "minDate")]
        public DateTime MinDate { get; set; } = new DateTime(2021, 1, 1);
        [DataMember(Name = "maxDate")]
        public DateTime MaxDate { get; set; } = DateTime.Now;
        public string EType { get; set; }
        public string EData { get; set; }
        public string ETime { get; set; }
        public List<string> Owns { get; set; } = new List<string>();
        public ICollection<Owner> Owners { get; set; } = new List<Owner>();
        public Owner? Owner { get; set; } = new Owner();
        public int? OwnerId { get; set; }
        public bool OwnerChecked { get; set; } = true;
        public List<string> Estabs { get; set; } = new List<string>();
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();
        public int? EstablishmentId { get; set; }
        public Establishment? Establishment { get; set; } = new Establishment();
        public List<string> Cats { get; set; } = new List<string>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public int? CategoryId { get; set; }
        public Category? Category { get; set; } = new Category();
        public ICollection<Movement>? Movements { get; set; }

        public ChartViewModel() 
        {

		}
    }
}
