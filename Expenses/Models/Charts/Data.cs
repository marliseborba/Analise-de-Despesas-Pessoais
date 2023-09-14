namespace Expenses.Models.Charts
{
	public class Data
	{
		public List<DataSet> datasets { get; set; } = new List<DataSet>();

		public Data() 
		{
		}

		public Data(List<DataSet> datasets)
		{
			this.datasets = datasets;
		}
	}
}
