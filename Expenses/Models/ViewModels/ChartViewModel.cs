using Newtonsoft.Json;

namespace Expenses.Models.ViewModels
{
    public class ChartViewModel
    {
        public string ChartJ { get; set; }
		public string axisYJ { get; set; }
		public string axisXJ { get; set; }
		public Chart Chart { get; set; }
        public string DataSeriesJ { get; set; }
        public List<DataSerie> DataSeries { get; set; } = new List<DataSerie>();
        public string DataPoints { get; set; }

        public ChartViewModel() 
        {
			//axisYJ = JsonConvert.SerializeObject(Chart.axisY);
			//axisXJ = JsonConvert.SerializeObject(Chart.axisX);
		}
    }
}
