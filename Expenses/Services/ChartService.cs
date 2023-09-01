using Expenses.Data;
using Expenses.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Expenses.Services
{
	public class ChartService
	{
		private readonly ExpensesContext _context;
		private static MovementService _movementService;
		private static Random random = new Random(DateTime.Now.Millisecond);
		private static List<DataPoint> _dataPoints;

		public ChartService(ExpensesContext context, MovementService movementService)
		{
			_context = context;
			_movementService = movementService;
		}

		public static List<DataPoint> GetRandomDataForNumericAxis(int count)
		{
			double y = 50;
			_dataPoints = new List<DataPoint>();


			for (int i = 0; i < count; i++)
			{
				y = y + (random.Next(0, 20) - 10);

				_dataPoints.Add(new DataPoint(i, y));
			}

			return _dataPoints;
		}

		public static List<int> ConvertDateToMonth(List<DateTime> dates)
		{
			List<int> months = new List<int>();
			foreach (DateTime date in dates) 
			{
				var month = date.Month;
				months.Add(month);
			}

			return months;
		}

		public static List<DataPoint> FormatToDataPoint(List<Movement> movements)
		{
			double jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec;
			jan = feb = mar = apr = may = jun = jul = aug = sep = oct = nov = dec = 0.0;

			foreach (Movement movement in movements)
			{
				var month = movement.Date.Month;
				switch (month)
				{
					case 1:
						jan += movement.Value;
						break;
					case 2:
						feb += movement.Value;
						break;
					case 3:
						mar += movement.Value;
						break;
					case 4:
						apr += movement.Value;
						break;
					case 5:
						may += movement.Value;
						break;
					case 6:
						jun += movement.Value;
						break;
					case 7:
						jul += movement.Value;
						break;
					case 8:
						aug += movement.Value;
						break;
					case 9:
						sep += movement.Value;
						break;
					case 10:
						oct += movement.Value;
						break;
					case 11:
						nov += movement.Value;
						break;
					case 12:
						dec += movement.Value;
						break;
				}
			}

			List<DataPoint> dataPoints = new List<DataPoint>();
			dataPoints.Add(new DataPoint(1, jan));
			dataPoints.Add(new DataPoint(2, feb));
			dataPoints.Add(new DataPoint(3, mar));
			dataPoints.Add(new DataPoint(4, apr));
			dataPoints.Add(new DataPoint(5, may));
			dataPoints.Add(new DataPoint(6, jun));
			dataPoints.Add(new DataPoint(7, jul));
			dataPoints.Add(new DataPoint(8, aug));
			dataPoints.Add(new DataPoint(9, sep));
			dataPoints.Add(new DataPoint(10, oct));
			dataPoints.Add(new DataPoint(11, nov));
			dataPoints.Add(new DataPoint(12, dec));


			return dataPoints;
		}

	}
}
