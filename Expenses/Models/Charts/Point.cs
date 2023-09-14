namespace Expenses.Models.Charts
{
	public class Point
	{
		public string x { get; set; }
		public double y { get; set; }

		public Point()
		{
		}

		public Point(string x, double y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
