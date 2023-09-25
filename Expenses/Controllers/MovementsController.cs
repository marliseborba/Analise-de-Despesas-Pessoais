using Expenses.Data;
using Expenses.Models;
using Expenses.Models.ViewModels;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Expenses.Controllers
{
    public class MovementsController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly SeedingService _seedingService;
        private readonly MovementService _movementService;
        private readonly CategoryService _categoryService;
        private readonly EstablishmentService _establishmentService;
        private readonly OwnerService _ownerService;

        public MovementsController(IMemoryCache memoryCache,
            SeedingService seedingService,
            MovementService movementService,
            CategoryService categoryService,
            EstablishmentService establishmentService,
            OwnerService ownerService)
        {
            _memoryCache = memoryCache;
            _seedingService = seedingService;
            _movementService = movementService;
            _categoryService = categoryService;
            _establishmentService = establishmentService;
            _ownerService = ownerService;
        }

        public IActionResult Index()
        {
            _seedingService.SeedFake();
            //_movementService.UpdateEstablishments();
            //var catUpdated = _movementService.UpdateCategories();
            //TempData["catUpdated"] = catUpdated.Count;
            var viewModel = new MovementViewModel
            {
                Movements = _movementService.GetMovementsNoGrouping()
            };

            _movementService.PopulateViewModel(viewModel);

            return View("Index", viewModel);
        }

        public IActionResult SearchMovements(MovementViewModel viewModel)
        {
            List<Movement> movs = new List<Movement>();
            movs = _movementService.SearchMovements(viewModel).ToList();
            viewModel.Movements = movs;
            _movementService.PopulateViewModel(viewModel);
            return View("Index", viewModel);
        }

        public IActionResult Upload()
        {
            //_seedingService.Seed();
            //_movementService.UpdateEstablishments();
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IList<IFormFile> files)
        {
            _movementService._memoryCache.Set("files", files);
            List<Movement> movs = _movementService.UploadExtract(files).ToList();
            return View(movs);
        }

        public IActionResult SaveInvoice()
        {
            var saves = _movementService.SaveInvoice();
            TempData["saved"] = saves.Count;
            _movementService.UpdateEstablishments();
            _movementService.UpdateCategories();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = _movementService.FindByDate(minDate, maxDate);
            return View(nameof(Index), result);
        }

        [HttpPost]
        public void Create(Movement movement)
        {
            _movementService.Insert(movement);
        }

        public void Edit(Movement movement)
        {

        }

        [HttpPost]
        public void Edit(List<Movement> movements)
        {
            _movementService.Insert(movements);
        }
    }
}
