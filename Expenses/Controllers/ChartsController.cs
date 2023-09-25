using Expenses.Data;
using Expenses.Models;
using Expenses.Models.Charts;
using Expenses.Models.ViewModels;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Expenses.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ExpensesContext _context;
        private readonly HomeService _homeService;
        private readonly ChartService _chartService;
        private readonly MovementService _movementService;
        private readonly EstablishmentService _establishmentService;
		private readonly CategoryService _categoryService;
		private readonly OwnerService _ownerService;

		public ChartsController(ExpensesContext expensesContext,
            HomeService homeService,
            ChartService chartService,
            MovementService movementService,
            EstablishmentService establishmentService,
            CategoryService categoryService,
            OwnerService ownerService)
		{
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
            ChartViewModel chartViewModel = new ChartViewModel();
            chartViewModel.Categories = _context.Category.ToList();
            chartViewModel.Establishments = _context.Establishment.ToList();
            chartViewModel.Owners = _context.Owner.ToList();

            chartViewModel.EType = "bar";
            chartViewModel.EData = "Categoria";
            chartViewModel.ETime = "Meses";
            chartViewModel.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 2, 1);
            chartViewModel.MaxDate = DateTime.Now;
            chartViewModel.ChartJ = _chartService.Chart(chartViewModel);
            return View(chartViewModel);
        }


        public IActionResult Search(ChartViewModel viewModel, List<string> cats, List<string> estabs, List<string> owns)
        {
            viewModel.Cats = cats;
            viewModel.Estabs = estabs;
            viewModel.Owns = owns;
            viewModel.ChartJ = _chartService.Chart(viewModel);
            viewModel.Establishments = _establishmentService.GetEstablishments();
			viewModel.Categories = _categoryService.GetCategories();
			viewModel.Owners = _ownerService.GetOwners();
            return View("Index", viewModel);
        }
    }
}
