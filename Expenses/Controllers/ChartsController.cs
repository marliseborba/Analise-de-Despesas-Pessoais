using Expenses.Models;
using Expenses.Models.ViewModels;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Expenses.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ChartService _chartService;
        private readonly MovementService _movementService;

		public ChartsController(ChartService chartService, MovementService movementService)
		{
			_chartService = chartService;
            _movementService = movementService;
		}

		public IActionResult Index()
        {
            var viewModel = new ChartViewModel();
            Chart chart = new Chart();
            DataSerie dataSerie = new DataSerie();
            DataSerie dataSerie2 = new DataSerie();
            List<DataSerie> dataSeries = new List<DataSerie>();
            List<DataPoint> dataPoints = new List<DataPoint>();
			List<Movement> movements = new List<Movement>();

			movements = _movementService.GetMovementsNoGrouping().Where(x => x.Date.Year == 2022).ToList();

			dataPoints = ChartService.FormatToDataPoint(movements).ToList();

            dataSerie.dataPoints = dataPoints;

            chart.data.Add(dataSerie);
            chart.data.Add(dataSerie2);
            viewModel.Chart = chart;

            dataPoints = ChartService.GetRandomDataForNumericAxis(30);
			List<DataPoint> dataPoints2 = ChartService.GetRandomDataForNumericAxis(30);
			dataSerie.dataPoints = dataPoints;

            dataSerie2.dataPoints = dataPoints2;
			chart.data.Add(dataSerie2);
			viewModel.DataSeries.Add(dataSerie);
			viewModel.DataSeries.Add(dataSerie2);

			viewModel.DataSeriesJ = JsonConvert.SerializeObject(viewModel.DataSeries);
			//viewModel.DataSeriesJ = JsonConvert.SerializeObject(dataSerie);
			viewModel.ChartJ = JsonConvert.SerializeObject(chart);
            viewModel.DataPoints = JsonConvert.SerializeObject(dataPoints);
			ViewBag.DataSeries = JsonConvert.SerializeObject(dataSerie);
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			//viewModel.Chart.DataSeries.Add(new DataSerie());
			//ViewBag.DataPoints = viewModel.DataSeries.FirstOrDefault().dataPoints;
			//ViewBag.DataPoints = JsonConvert.SerializeObject(viewModel.DataSeries.FirstOrDefault().dataPoints);
			//ViewBag.DataSeries = JsonConvert.SerializeObject(viewModel.DataSeries.ToList());

			return View(viewModel);
        }

		public ActionResult Line()
		{
			//Below code can be used to include dynamic data in Chart. Check view page and uncomment the line "dataPoints: @Html.Raw(ViewBag.DataPoints)"
			//ViewBag.DataPoints = JsonConvert.SerializeObject(DataService.GetRandomDataForNumericAxis(1000), _jsonSetting);

			return View();
		}

		[HttpPost]
        public JsonResult Chart() 
        {
            /*
            List<Chart> charts = new List<Chart>();
            Chart c1 = new Chart() { Movement = "Mercado", Value = 840.4 };
            Chart c2 = new Chart() { Movement = "Farmácia", Value = 200.4 };
            Chart c3 = new Chart() { Movement = "Transporte", Value = 150 };
            Chart c4 = new Chart() { Movement = "Mercado", Value = 400.0 };
            charts.Add(c1);
            charts.Add(c2);
            charts.Add(c3);
            charts.Add(c4);
            */
            List<object> charts = new List<object>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Categoria", System.Type.GetType("System.String"));
            dt.Columns.Add("Valor", System.Type.GetType("System.Double"));

            DataRow dr = dt.NewRow();
            dr["Categoria"] = "Mercado";
            dr["Valor"] = 880.0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Categoria"] = "Farmácia";
            dr["Valor"] = 180.0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Categoria"] = "Bichos";
            dr["Valor"] = 250.0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Categoria"] = "Transporte";
            dr["Valor"] = 136.0;
            dt.Rows.Add(dr);

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                charts.Add(x);
            }

            return Json(charts);
        }

        [HttpPost]
        public JsonResult NovoGrafico()
        {
            List<object> iDados = new List<object>();
            //Criando dados de exemplo
            DataTable dt = new DataTable();
            dt.Columns.Add("Vendas", System.Type.GetType("System.String"));
            dt.Columns.Add("Unidades", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();
            dr["Vendas"] = "Chevrolet Onix";
            dr["Unidades"] = 171;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Vendas"] = "Huinday HB20";
            dr["Unidades"] = 96;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Vendas"] = "Ford Ka(Hatch)";
            dr["Unidades"] = 87;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Vendas"] = "WolksVagem Gol";
            dr["Unidades"] = 67;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Vendas"] = "Renaul Sandero";
            dr["Unidades"] = 63;
            dt.Rows.Add(dr);

            //Percorrendo e extraindo cada DataColumn para List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iDados.Add(x);
            }
            //Dados retornados no formato JSON
            //return Json(iDados, JsonRequestBehavior.AllowGet);
            return Json(iDados);
        }
    }
}
