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
        private readonly KeyWordService _keyWordService;

        public CategoriesController(ExpensesContext expensesContext,
            SeedingService seedService,
            CategoryService categoryService,
            MovementService movementService,
            KeyWordService keyWordService)
        {
            _context = expensesContext;
            _seedService = seedService;
            _categoryService = categoryService;
            _movementService = movementService;
            _keyWordService = keyWordService;
        }

        public IActionResult Index()
        {
            _movementService.UpdateCategories();
            return View(_categoryService.GetCategories().ToList());
        }

        public IActionResult Create() 
        {
            var viewModel = new CategoryViewModel
            {
                KeyWords = _keyWordService.GetKeyWords().ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category, List<int> keys) 
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CategoryViewModel
                {
                    Category = category,
                    KeyWords = _keyWordService.GetKeyWords().ToList()
                };
                return View(viewModel);
            }
            _categoryService.Insert(category, keys);
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
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.Category = obj;
            viewModel.KeyWords = _context.KeyWord.ToList();
            //viewModel.Keys = obj.Keys.ToList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category, List<int> keys)
        {
            if (!ModelState.IsValid)
            {
                CategoryViewModel viewModel = new CategoryViewModel();
                viewModel.Category = category;
                viewModel.KeyWords = _context.KeyWord.ToList();
                return View(viewModel);
            }
            if (id != category.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível" });
            }
            try
            {
                var updated = _categoryService.Update(category, keys);
                TempData["updated"] = updated.Name;
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
