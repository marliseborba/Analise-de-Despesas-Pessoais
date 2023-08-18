using Expenses.Data;
using Expenses.Models;
using Expenses.Models.ViewModels;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Expenses.Controllers
{
    public class CategoriesController : Controller
    {
        public readonly ExpensesContext _context;
        private readonly SeedingService _seedService;
        private readonly CategoryService _categoryService;
        private readonly MovementService _movementService;

        public CategoriesController(ExpensesContext expensesContext, SeedingService seedService, CategoryService categoryService, MovementService movementService)
        {
            _context = expensesContext;
            _seedService = seedService;
            _categoryService = categoryService;
            _movementService = movementService;
        }

        public IActionResult Index()
        {
            //_seedService.Seed();
            return View(_categoryService.GetCategories().ToList());
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category) 
        {
            _categoryService.Insert(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = _categoryService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            if (id != category.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível" });
            }
            try
            {
                _categoryService.Update(category);
                _movementService.UpdateEstablishments();
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
    }
}
