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
