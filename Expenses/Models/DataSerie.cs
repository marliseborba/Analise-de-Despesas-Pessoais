using Expenses.Services;

namespace Expenses.Models
{
	public class DataSerie
	{
		public string type { get; set; }
		public string color { get; set; }
		public ICollection<DataPoint> dataPoints { get; set; } = new List<DataPoint>();
		public DataSerie()
		{
			type = "line";
		}
	}
}
