using Expenses.Data;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Controllers
{
    public class CategoriesController : Controller
    {
        public readonly ExpensesContext _context;
        private readonly SeedingService _seedService;
        private readonly CategoryService _categoryService;

        public CategoriesController(ExpensesContext expensesContext, SeedingService seedService, CategoryService category)
        {
            _context = expensesContext;
            _seedService = seedService;
            _categoryService = category;
        }

        public IActionResult Index()
        {
            //_seedService.Seed();
            return View(_context.Category.ToList());
        }
    }
}
