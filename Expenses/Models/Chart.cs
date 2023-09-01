using Expenses.Services;
using Newtonsoft.Json;

namespace Expenses.Models
{
	public class Chart
	{
        public ICollection<DataSerie> data { get; set; } = new List<DataSerie>();


		public Chart()
		{

        }
	}
}
