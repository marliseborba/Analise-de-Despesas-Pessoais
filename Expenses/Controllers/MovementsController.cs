using Expenses.Data;
using Expenses.Models;
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

        public MovementsController(IMemoryCache memoryCache, SeedingService seedingService, MovementService movementService)
        {
            _memoryCache = memoryCache;
            _seedingService = seedingService;
            _movementService = movementService;
        }

        public IActionResult Index()
        {
            ViewData["minDate"] = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewData["maxDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View(_movementService.GetMovements());
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IList<IFormFile> files)
        {
            _movementService._memoryCache.Set("file", files);
            List<Movement> movs = _movementService.Upload(files).ToList();
            return View(movs);
        }


        public IActionResult SaveInvoice()
        {
            return RedirectToAction(nameof(Index), _movementService.SaveInvoice());
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
