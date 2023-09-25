using Aspose.Finance.Xbrl;
using Expenses.Data;
using Expenses.Models;
using Expenses.Models.Charts;
using Expenses.Models.ViewModels;
using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.Serialization;

namespace Expenses.Services
{
    public class HomeService
    {
        private readonly ExpensesContext _context;
        private static MovementService _movementService;
        private readonly EstablishmentService _establishmentService;
        private readonly CategoryService _categoryService;
        private readonly OwnerService _ownerService;
        private readonly ChartService _chartService;

        public HomeService(ExpensesContext context,
            EstablishmentService establishmentService,
            CategoryService categoryService,
            OwnerService ownerService,
            ChartService chartService)
        {
            _context = context;
            _establishmentService = establishmentService;
            _categoryService = categoryService;
            _ownerService = ownerService;
            _chartService = chartService;
        }

        public string ChartBarMonth()
        {
            Chart chart = new Chart();
            Models.Charts.Data data = new Models.Charts.Data();
            List<DataSet> dataSets = new List<DataSet>();
            DataSet dataSet = new DataSet();
            List<Point> points = new List<Point>();

            data.datasets = dataSets;
            chart.data = data;
            chart.type = "bar";
            //chart.options.plugins.title.text = "Gastos por Categoria";
            string month = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
            month = char.ToUpper(month[0]) + month.Substring(1);
            //chart.options.plugins.subtitle.text = month;

            List <Category> categories = new List<Category>();
            categories = _context.Category.ToList();
            List<string> catNames = new List<string>();
            catNames = categories.Select(c => c.Name).ToList();

            foreach(var item in categories)
            {
                List<Movement> movements = new List<Movement>();
                movements = _context.Movement.Where(x => x.Date.Month == DateTime.Now.Month
                    && x.Date.Year == DateTime.Now.Year
                    && x.Categories.Any(x => x.Name.ToLower().Contains(item.Name.ToLower()))).ToList();
                if (movements.Count() > 0)
                {
                    List<double> values = new List<double>();
                    values = movements.Select(x => x.Value).ToList();
                    double sum = 0.0;
                    foreach(double v in values)
                    {
                        sum += _chartService.ToPositive(v);
                    }
                    points.Add(new Point(item.Name, sum));
                }
            }
            dataSet.label = month;
            dataSet.data = points;
            dataSets.Add(dataSet);

            return JsonConvert.SerializeObject(chart);
        }

        public string ChartBarTwoMonths()
        {
            Chart chart = new Chart();
            Models.Charts.Data data = new Models.Charts.Data();
            List<DataSet> dataSets = new List<DataSet>();
            DataSet dataSet1 = new DataSet();
            List<Point> points1 = new List<Point>();
            DataSet dataSet2 = new DataSet();
            List<Point> points2 = new List<Point>();

            data.datasets = dataSets;
            chart.data = data;
            chart.type = "bar";
            //chart.options.plugins.title.text = "Gastos por Categoria";
            string month = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
            string last = DateTime.Now.AddMonths(-1).ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
            month = char.ToUpper(month[0]) + month.Substring(1);
            last = char.ToUpper(last[0]) + last.Substring(1);
            //chart.options.plugins.subtitle.text = last + " e " + month;

            List<Category> categories = new List<Category>();
            categories = _context.Category.ToList();
            List<string> catNames = new List<string>();
            catNames = categories.Select(c => c.Name).ToList();

            foreach (var item in categories)
            {
                List<Movement> movements = new List<Movement>();
                movements = _context.Movement.Where(x => x.Date.Month == DateTime.Now.Month - 1
                    && x.Date.Year == DateTime.Now.Year
                    && x.Categories.Any(x => x.Name.ToLower().Contains(item.Name.ToLower()))).ToList();
                if (movements.Count() > 0)
                {
                    List<double> values = new List<double>();
                    values = movements.Select(x => x.Value).ToList();
                    double sum = 0.0;
                    foreach (double v in values)
                    {
                        sum += _chartService.ToPositive(v);
                    }
                    points1.Add(new Point(item.Name, sum));
                }
            }

            foreach (var item in categories)
            {
                List<Movement> movements = new List<Movement>();
                movements = _context.Movement.Where(x => x.Date.Month == DateTime.Now.Month
                    && x.Date.Year == DateTime.Now.Year
                    && x.Categories.Any(x => x.Name.ToLower().Contains(item.Name.ToLower()))).ToList();
                if (movements.Count() > 0)
                {
                    List<double> values = new List<double>();
                    values = movements.Select(x => x.Value).ToList();
                    double sum = 0.0;
                    foreach (double v in values)
                    {
                        sum += _chartService.ToPositive(v);
                    }
                    points2.Add(new Point(item.Name, sum));
                }
            }
            dataSet1.label = last;
            dataSet2.label = month;
            dataSet1.data = points1;
            dataSet2.data = points2;
            dataSets.Add(dataSet1);
            dataSets.Add(dataSet2);

            return JsonConvert.SerializeObject(chart);
        }

        public string ChartBarThreeMonths()
        {
            Chart chart = new Chart();
            Models.Charts.Data data = new Models.Charts.Data();
            List<DataSet> dataSets = new List<DataSet>();
            DataSet dataSet1 = new DataSet();
            List<Point> points1 = new List<Point>();
            DataSet dataSet2 = new DataSet();
            List<Point> points2 = new List<Point>();
            DataSet dataSet3 = new DataSet();
            List<Point> points3 = new List<Point>();

            data.datasets = dataSets;
            chart.data = data;
            chart.type = "bar";
            //chart.options.plugins.title.text = "Gastos por Categoria";
            string month = DateTime.Now.AddMonths(-5).ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
            string last = DateTime.Now.AddMonths(-6).ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
            string pen = DateTime.Now.AddMonths(-7).ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
            month = char.ToUpper(month[0]) + month.Substring(1);
            last = char.ToUpper(last[0]) + last.Substring(1);
            pen = char.ToUpper(pen[0]) + pen.Substring(1);
            //chart.options.plugins.subtitle.text = pen + ", " + last + " e " + month;

            List<Category> categories = new List<Category>();
            categories = _context.Category.ToList();
            List<string> catNames = new List<string>();
            catNames = categories.Select(c => c.Name).ToList();

            // Penúltimo mês
            foreach (var item in categories)
            {
                List<Movement> movements = new List<Movement>();
                movements = _context.Movement.Where(x => x.Date.Month == DateTime.Now.Month - 7
                    && x.Date.Year == DateTime.Now.Year
                    && x.Categories.Any(x => x.Name.ToLower().Contains(item.Name.ToLower()))).ToList();
                if (movements.Count() > 0)
                {
                    List<double> values = new List<double>();
                    values = movements.Select(x => x.Value).ToList();
                    double sum = 0.0;
                    foreach (double v in values)
                    {
                        sum += _chartService.ToPositive(v);
                    }
                    points1.Add(new Point(item.Name, sum));
                }
            }

            // Último mês
            foreach (var item in categories)
            {
                List<Movement> movements = new List<Movement>();
                movements = _context.Movement.Where(x => x.Date.Month == DateTime.Now.Month - 6
                    && x.Date.Year == DateTime.Now.Year
                    && x.Categories.Any(x => x.Name.ToLower().Contains(item.Name.ToLower()))).ToList();
                if (movements.Count() > 0)
                {
                    List<double> values = new List<double>();
                    values = movements.Select(x => x.Value).ToList();
                    double sum = 0.0;
                    foreach (double v in values)
                    {
                        sum += _chartService.ToPositive(v);
                    }
                    points2.Add(new Point(item.Name, sum));
                }
            }

            // Mês atual
            foreach (var item in categories)
            {
                List<Movement> movements = new List<Movement>();
                movements = _context.Movement.Where(x => x.Date.Month == DateTime.Now.Month - 5
                    && x.Date.Year == DateTime.Now.Year
                    && x.Categories.Any(x => x.Name.ToLower().Contains(item.Name.ToLower()))).ToList();
                if (movements.Count() > 0)
                {
                    List<double> values = new List<double>();
                    values = movements.Select(x => x.Value).ToList();
                    double sum = 0.0;
                    foreach (double v in values)
                    {
                        sum += _chartService.ToPositive(v);
                    }
                    points3.Add(new Point(item.Name, sum));
                }
            }
            dataSet1.label = pen;
            dataSet2.label = last;
            dataSet3.label = month;
            dataSet1.data = points1;
            dataSet2.data = points2;
            dataSet3.data = points3;
            dataSets.Add(dataSet1);
            dataSets.Add(dataSet2);
            dataSets.Add(dataSet3);

            return JsonConvert.SerializeObject(chart);
        }

        public string ChartPie()
        {
            ChartPie chart = new ChartPie();
            Models.Charts.ChartPie.Data data = new Models.Charts.ChartPie.Data();
            List<Models.Charts.ChartPie.Data.DataSet> dataSets = new List<Models.Charts.ChartPie.Data.DataSet>();
            Models.Charts.ChartPie.Data.DataSet dataSet = new Models.Charts.ChartPie.Data.DataSet();

            List<string> labels = new List<string>();
            List<double> points = new List<double>();

            chart.type = "doughnut";
            //chart.options.plugins.title.text = "Gastos por Categoria";
            string month = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
            month = char.ToUpper(month[0]) + month.Substring(1);
            //chart.options.plugins.subtitle.text = month;

            List<Category> categories = new List<Category>();
            categories = _context.Category.ToList();
            List<string> catNames = new List<string>();
            catNames = categories.Select(c => c.Name).ToList();

            foreach (var item in categories)
            {
                List<Movement> movements = new List<Movement>();
                movements = _context.Movement.Where(x => x.Value < 0 && x.Date.Month == DateTime.Now.Month
                    && x.Date.Year == DateTime.Now.Year
                    && x.Categories.Any(x => x.Name.ToLower().Contains(item.Name.ToLower()))).ToList();
                if (movements.Count() > 0)
                {
                    List<double> values = new List<double>();
                    values = movements.Select(x => x.Value).ToList();
                    double sum = 0.0;
                    foreach (double v in values)
                    {
                        sum += _chartService.ToPositive(v);
                    }
                    labels.Add(item.Name);
                    points.Add(sum);
                }
            }

            dataSet.data = points;
            data.labels = labels;
            dataSets.Add(dataSet);
            data.datasets = dataSets;
            chart.data = data;

            return JsonConvert.SerializeObject(chart);
        }

        public string ChartPieEstabs()
        {
            ChartPie chart = new ChartPie();
            Models.Charts.ChartPie.Data data = new Models.Charts.ChartPie.Data();
            List<Models.Charts.ChartPie.Data.DataSet> dataSets = new List<Models.Charts.ChartPie.Data.DataSet>();
            Models.Charts.ChartPie.Data.DataSet dataSet = new Models.Charts.ChartPie.Data.DataSet();

            List<string> labels = new List<string>();
            List<double> points = new List<double>();

            chart.type = "doughnut";
            //chart.options.plugins.title.text = "Gastos por Estabelecimento";
            string month = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
            month = char.ToUpper(month[0]) + month.Substring(1);
            //chart.options.plugins.subtitle.text = month;

            List<Establishment> establishments = new List<Establishment>();
            establishments = _context.Establishment.ToList();
            List<string> estabNames = new List<string>();
            estabNames = establishments.Select(c => c.Name).ToList();

            foreach (var item in establishments)
            {
                List<Movement> movements = new List<Movement>();
                movements = _context.Movement.Where(x => x.Value < 0 && x.Date.Month == DateTime.Now.Month
                    && x.Date.Year == DateTime.Now.Year
                    && x.Establishment.Name.ToLower().Contains(item.Name.ToLower())).ToList();
                if (movements.Count() > 0)
                {
                    List<double> values = new List<double>();
                    values = movements.Select(x => x.Value).ToList();
                    double sum = 0.0;
                    foreach (double v in values)
                    {
                        sum += _chartService.ToPositive(v);
                    }
                    labels.Add(item.Name);
                    points.Add(sum);
                }
            }

            dataSet.data = points;
            data.labels = labels;
            dataSets.Add(dataSet);
            data.datasets = dataSets;
            chart.data = data;

            return JsonConvert.SerializeObject(chart);
        }
    }
}
