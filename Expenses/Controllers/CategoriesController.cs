using Expenses.Services;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly SeedService _seedService;

        public CategoriesController(SeedService seedService)
        {
            _seedService = seedService;
        }

        public IActionResult Index()
        {
            return View(_seedService.CategorySeeding());
        }
    }
}
