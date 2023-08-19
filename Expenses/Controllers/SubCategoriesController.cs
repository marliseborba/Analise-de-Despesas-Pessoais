using Expenses.Data;
using Expenses.Models;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Expenses.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly ExpensesContext _context;
        private readonly SubCategoryService _subCategoryService;
        private readonly MovementService _movementService;

        public SubCategoriesController(ExpensesContext context, SubCategoryService subCategoryService, MovementService movementService)
        {
            _context = context;
            _subCategoryService = subCategoryService;
            _movementService = movementService;
        }

        public IActionResult Index()
        {
            return View(_subCategoryService.GetSubCategories().ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SubCategory subCategory)
        {
            _subCategoryService.Insert(subCategory);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = _subCategoryService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(subCategory);
            }
            if (id != subCategory.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível" });
            }
            try
            {
                _subCategoryService.Update(subCategory);
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
