using Expenses.Models;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Expenses.Controllers
{
    public class MovementsController : Controller
    {
        private readonly MovementService _movementService;

        public MovementsController(MovementService movementService)
        {
            _movementService = movementService;
        }

        public IActionResult Index()
        {
            return View(_movementService.Upload());
        }
    }
}
