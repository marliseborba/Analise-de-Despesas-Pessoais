using Expenses.Services;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Controllers
{
    public class EstablishmentsController : Controller
    {
        private readonly SeedService _seedService;

        public EstablishmentsController(SeedService seedService)
        {
            _seedService = seedService;
        }

        public IActionResult Index()
        {
            return View(_seedService.EstablishmentSeeding());
        }
    }
}
