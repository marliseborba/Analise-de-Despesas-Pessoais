using Expenses.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Expenses.Models.ViewModels;
using Expenses.Models.Charts;
using Expenses.Services;
using Newtonsoft.Json;
using Expenses.Data;
using System.Web;
using System.Net;

namespace Expenses.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpensesContext _context;
        private readonly HomeService _homeService;
        private readonly ChartService _chartService;
        private readonly MovementService _movementService;
        private readonly EstablishmentService _establishmentService;
        private readonly CategoryService _categoryService;
        private readonly OwnerService _ownerService;

        public HomeController(ILogger<HomeController> logger,
            ExpensesContext expensesContext,
            HomeService homeService,
            ChartService chartService,
            MovementService movementService,
            EstablishmentService establishmentService,
            CategoryService categoryService,
            OwnerService ownerService)
        {
            _logger = logger;
            _context = expensesContext;
            _homeService = homeService;
            _chartService = chartService;
            _movementService = movementService;
            _establishmentService = establishmentService;
            _categoryService = categoryService;
            _ownerService = ownerService;
        }

        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();

            // Chart Pie Mês-Categorias
            ChartViewModel chartViewModelPieMonthCat = new ChartViewModel();
            chartViewModelPieMonthCat.EType = "doughnut";
            chartViewModelPieMonthCat.EData = "Categoria";
            chartViewModelPieMonthCat.ETime = "Meses";
            chartViewModelPieMonthCat.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            chartViewModelPieMonthCat.MaxDate = DateTime.Now;
            viewModel.ChartPieCats = _chartService.Chart(chartViewModelPieMonthCat);

            // Chart Pie Mês-Estabelecimentos
            ChartViewModel chartViewModelPieMonthEstab = new ChartViewModel();
            chartViewModelPieMonthEstab.EType = "doughnut";
            chartViewModelPieMonthEstab.EData = "Estabelecimento";
            chartViewModelPieMonthEstab.ETime = "Meses";
            chartViewModelPieMonthEstab.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            chartViewModelPieMonthEstab.MaxDate = DateTime.Now;
            viewModel.ChartPieEstabs = _chartService.Chart(chartViewModelPieMonthEstab);

            // Chart Bar - 3 Mêses - Categorias
            ChartViewModel chartViewModelBarThreeMonthsCat = new ChartViewModel();
            chartViewModelBarThreeMonthsCat.EType = "bar";
            chartViewModelBarThreeMonthsCat.EData = "Categoria";
            chartViewModelBarThreeMonthsCat.ETime = "Meses";
            chartViewModelBarThreeMonthsCat.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month-2,1);
            chartViewModelBarThreeMonthsCat.MaxDate = DateTime.Now;
            viewModel.ChartBarThreeMonths = _chartService.Chart(chartViewModelBarThreeMonthsCat);

            List<Movement> credits = _movementService.GetMovementsNoGrouping().Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month && x.Value > 0).ToList();
            List<Movement> debits = _movementService.GetMovementsNoGrouping().Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month && x.Value < 0).ToList();
            viewModel.CreditMarlise = credits.Where(x => x.Owner.Name.Equals("Fulana")).Sum(x => x.Value);
            viewModel.CreditMonique = credits.Where(x => x.Owner.Name.Equals("Ciclana")).Sum(x => x.Value);
            viewModel.DebitMarlise = _chartService.ToPositive(debits.Where(x => x.Owner.Name.Equals("Fulana")).Sum(x => x.Value));
            viewModel.DebitMonique = _chartService.ToPositive(debits.Where(x => x.Owner.Name.Equals("Ciclana")).Sum(x => x.Value));

            return View(viewModel);
            /*
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.ChartBarMonth = _homeService.ChartBarMonth();
            viewModel.ChartBarTwoMonths = _homeService.ChartBarTwoMonths();
            viewModel.ChartBarThreeMonths = _homeService.ChartBarThreeMonths();
            viewModel.ChartPie = _homeService.ChartPie();
            viewModel.ChartPieEstabs = _homeService.ChartPieEstabs();
            return View(viewModel);
            //return View(_chartService.Example());
            */
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}