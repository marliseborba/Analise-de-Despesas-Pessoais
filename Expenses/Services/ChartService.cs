using Aspose.Finance.Xbrl;
using Expenses.Data;
using Expenses.Models;
using Expenses.Models.Charts;
using Expenses.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Data.SqlTypes;
using System.Globalization;
using static Expenses.Models.Charts.ChartPie.Option.Plugins.Subtitle;

namespace Expenses.Services
{
	public class ChartService
	{
		private readonly ExpensesContext _context;
		private static MovementService _movementService;
        private readonly EstablishmentService _establishmentService;
        private readonly CategoryService _categoryService;
        private readonly OwnerService _ownerService;

        public ChartService(ExpensesContext context,
            MovementService movementService,
            EstablishmentService establishmentService,
            CategoryService categoryService,
            OwnerService ownerService)
		{
			_context = context;
			_movementService = movementService;
            _establishmentService = establishmentService;
            _categoryService = categoryService;
            _ownerService = ownerService;
		}

        public string Chart(ChartViewModel viewModel)
        {
            Chart chart = new Chart();
            Models.Charts.Data data = new Models.Charts.Data();
            List<DataSet> dataSets = new List<DataSet>();

            string type = viewModel.EType;
            string dataT = viewModel.EData;
            string time = viewModel.ETime;

            data.datasets = dataSets;
            chart.data = data;
            chart.type = type;
            chart.options.plugins.title.text = "Gastos por " + dataT;
            chart.options.plugins.subtitle = new Subtitle(viewModel.MinDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - " + viewModel.MaxDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

            MovementViewModel movView = new MovementViewModel();
            movView.MinDate = viewModel.MinDate;
            movView.MaxDate = viewModel.MaxDate;
            movView.Owner = viewModel.Owner;
            movView.Establishment = viewModel.Establishment;
            movView.Category = viewModel.Category;
            movView.Cats = viewModel.Cats;
            movView.Owns = viewModel.Owns;
            movView.Estabs = viewModel.Estabs;
            //movView.Movements = null;
            List<Movement> movements = new List<Movement>();
            movements = _movementService.SearchMovements(movView).ToList();

            //viewModel.Movements =

            // Cria argumentos para função FormatToPoint
            List<string> datas = new List<string>();
            if(dataT.Equals("Pessoa"))
            {
                datas = viewModel.Owns;
                if (datas.Count == 1 && datas.FirstOrDefault().Equals("Selecione..."))
                {
                    datas = _context.Owner.Select(x => x.Name).ToList();
                }
                datas.Add("Owner");
            }
            if(dataT.Equals("Categoria"))
            {
                datas = viewModel.Cats;
                if (datas.Count == 1 && datas.FirstOrDefault().Equals("Selecione..."))
                {
                    datas = _context.Category.Select(x => x.Name).ToList();
                }
                datas.Add("Category");
            }
            if (dataT.Equals("Estabelecimento"))
            {
                datas = viewModel.Estabs;
                if (datas.Count == 1 && datas.FirstOrDefault().Equals("Selecione..."))
                {
                    datas = _context.Establishment.Select(x => x.Name).ToList();
                }
                datas.Add("Establishment");
            }

            List<Object> times = new List<object>();
            if(time.Equals("Anos"))
            {
                for(int i = viewModel.MinDate.Year; i <= viewModel.MaxDate.Year; i++)
                {
                    times.Add(i);
                }
                times.Add("Year");
            }
            if (time.Equals("Meses"))
            {
                for (int i = 1; i <= 12; i++)
                {
                    times.Add(i);
                }
                times.Add("Month");
            }

            if (type.Equals("bar"))
            {
                data.datasets = FormatToPoint(movements, datas, times);
                chart.options.borderRadius = 3;
            }

            if (type.Equals("line"))
            {
                data.datasets = FormatToPoint(movements, datas, times);
            }

            return JsonConvert.SerializeObject(chart);
        }

        public List<DataSet> FormatToPoint(List<Movement> movements, List<string> datas, List<Object> times)
        {
            List<DataSet> dataSets = new List<DataSet>();

            string type = datas.LastOrDefault();
            string time = times.LastOrDefault().ToString();

            // Eixo X: Ano/Mês e DataSet: Owner/Categoria/Estab
            if(type.Equals("Owner"))
            {
                datas.Remove(type);
                foreach (var item in datas)
                {
                    DataSet dataSet = new DataSet();
                    List<Point> points = new List<Point>();
                    if (time.Equals("Year"))
                    {
                        times.Remove(time);
                        foreach (var i in times)
                        {
                            List<Movement> movs = new List<Movement>();
                            movs = movements.Where(x => x.Date.Year == (int)i && x.Owner.Name.ToLower().Contains(item.ToLower())).ToList();
                            double sum = ToPositive(movs.Sum(x => x.Value));
                            if (sum > 0)
                            {
                                points.Add(new Point(i.ToString(), sum));
                            }
                        }
                        dataSet.data = points;
                        dataSet.label = item.ToString();
                    }
                    if (time.Equals("Month"))
                    {
                        times.Remove(time);
                        foreach (var i in times)
                        {
                            List<Movement> movs = new List<Movement>();
                            movs = movements.Where(x => x.Date.Month == (int)i && x.Owner.Name.ToLower().Contains(item.ToLower())).ToList();
                            string month = new DateTime(2023, (int)i, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
                            month = char.ToUpper(month[0]) + month.Substring(1);
                            double sum = ToPositive(movs.Sum(x => x.Value));
                            if (sum > 0)
                            {
                                points.Add(new Point(month, sum));
                            }
                        }
                        dataSet.data = points;
                        dataSet.label = item.ToString();
                    }
                    if (dataSet.data.Count > 0)
                    {
                        dataSets.Add(dataSet);
                    }
                }
            }
        
            if (type.Equals("Category"))
            {
                datas.Remove(type);
                foreach (var item in datas)
                {
                    DataSet dataSet = new DataSet();
                    List<Point> points = new List<Point>();
                    if (time.Equals("Year"))
                    {
                        times.Remove(time);
                        foreach (var i in times)
                        {
                            List<Movement> movs = new List<Movement>();
                            movs = movements.Where(x => x.Date.Year == (int)i && x.Categories.Any(x => x.Name.ToLower().Contains(item.ToLower()))).ToList();
                            double sum = ToPositive(movs.Sum(x => x.Value));
                            if (sum > 0)
                            {
                                points.Add(new Point(i.ToString(), sum));
                            }
                        }
                        dataSet.data = points;
                        dataSet.label = item.ToString();
                    }
                    if (time.Equals("Month"))
                    {
                        times.Remove(time);
                        foreach (var i in times)
                        {
                            List<Movement> movs = new List<Movement>();
                            movs = movements.Where(x => x.Date.Month == (int)i && x.Categories.Any(x => x.Name.ToLower().Contains(item.ToLower()))).ToList();
                            string month = new DateTime(2023, (int)i, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
                            month = char.ToUpper(month[0]) + month.Substring(1);
                            double sum = ToPositive(movs.Sum(x => x.Value));
                            if (sum > 0)
                            {
                                points.Add(new Point(month, sum));
                            }
                        }
                        dataSet.data = points;
                        dataSet.label = item.ToString();
                    }
                    if(dataSet.data.Count > 0)
                    {
                        dataSets.Add(dataSet);
                    }
                }
            }

            if (type.Equals("Establishment"))
            {
                datas.Remove(type);
                foreach (var item in datas)
                {
                    DataSet dataSet = new DataSet();
                    List<Point> points = new List<Point>();
                    if (time.Equals("Year"))
                    {
                        times.Remove(time);
                        foreach (var i in times)
                        {
                            List<Movement> movs = new List<Movement>();
                            movs = movements.Where(x => x.Date.Year == (int)i && x.Establishment.Name.ToLower().Contains(item.ToLower())).ToList();
                            double sum = ToPositive(movs.Sum(x => x.Value));
                            if (sum > 0)
                            {
                                points.Add(new Point(i.ToString(), sum));
                            }
                        }
                        dataSet.data = points;
                        dataSet.label = item.ToString();
                    }
                    if (time.Equals("Month"))
                    {
                        times.Remove(time);
                        foreach (var i in times)
                        {
                            List<Movement> movs = new List<Movement>();
                            movs = movements.Where(x => x.Date.Month == (int)i && x.Establishment.Name.ToLower().Contains(item.ToLower())).ToList();
                            string month = new DateTime(2023, (int)i, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br"));
                            month = char.ToUpper(month[0]) + month.Substring(1);
                            double sum = ToPositive(movs.Sum(x => x.Value));
                            if (sum > 0)
                            {
                                points.Add(new Point(month, sum));
                            }
                        }
                        dataSet.data = points;
                        dataSet.label = item.ToString();
                    }
                    if (dataSet.data.Count > 0)
                    {
                        dataSets.Add(dataSet);
                    }
                }
            }
            return dataSets;
        }

        public ChartViewModel Example()
        {
            var viewModel = new ChartViewModel();

            viewModel.Categories = _context.Category.ToList();
            viewModel.Establishments = _context.Establishment.ToList();
            viewModel.Owners = _context.Owner.ToList();
            viewModel.MinDate = new DateTime(DateTime.Now.Year-3,1,1);
            viewModel.MaxDate = DateTime.Now;

            Expenses.Models.Charts.Chart chart = new Expenses.Models.Charts.Chart();
            Expenses.Models.Charts.Data data = new Expenses.Models.Charts.Data();
            List<DataSet> dataSets = new List<DataSet>();
            data.datasets = dataSets;
            chart.data = data;
            viewModel.Chart = chart;

            List<string> cats = new List<string>();
            cats.Add("Mercado");
            cats.Add("Farmácia");
            cats.Add("Lanches");
            cats.Add("Category");

            List<Object> times = new List<Object>();
            times.Add(DateTime.Now.Year-2);
            times.Add(DateTime.Now.Year-1);
            times.Add(DateTime.Now.Year);
            times.Add("Year");

            List<Movement> movements = new List<Movement>();
            movements = _context.Movement.Where(x => x.Categories.Any(c => cats.Contains(c.Name))).Include(x => x.Categories).ToList();

            data.datasets = FormatToPoint(movements, cats, times);
            
            viewModel.ChartJ = JsonConvert.SerializeObject(chart);

            return viewModel;
        }

        public double ToPositive(double num)
        {
            if (num < 0)
            {
                return num * -1;
            }
            return num;
        }
    }
}
