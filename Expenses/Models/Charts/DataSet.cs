namespace Expenses.Models.Charts
{
	public class DataSet
	{
		public string label { get; set; } = "";
        public string yAxisID { get; set; } = "yAxis";
        public List<Point> data { get; set; } = new List<Point>();

		public DataSet() 
		{ 
		}

		public DataSet(string label, List<Point> data)
		{
			this.label = label;
			this.data = data;

        }
	}
}
