using Expenses.Data;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Controllers
{
    public class EstablishmentsController : Controller
    {
        private readonly SeedingService _seedService;
        private readonly EstablishmentService _establishmentService;

        public EstablishmentsController(SeedingService seedService, EstablishmentService establishmentService)
        {
            _seedService = seedService;
            _establishmentService = establishmentService;
        }

        public IActionResult Index()
        {
            return View(_establishmentService.GetEstablishments());
        }
    }
}
