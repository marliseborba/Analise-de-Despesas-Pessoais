using Expenses.Services;

namespace Expenses.Models
{
	public class Chart
	{
		public string title { get; set; }
		public AxisY axisY { get; set; }
		public AxisX axisX { get; set; }
		public ICollection<DataSerie> data { get; set; } = new List<DataSerie>();


		public Chart()
		{
			title = "Gráfico Teste";
			axisX = new AxisX();
			axisY = new AxisY();
			axisX.valueFormatString = "MMM";
			axisY.valueFormatString = "#.###,##";
			axisY.prefix = "R$";
		}
	}
}
