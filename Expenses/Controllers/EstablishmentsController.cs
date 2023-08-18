using Expenses.Data;
using Expenses.Services;
using Expenses.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Expenses.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Expenses.Controllers
{
    public class EstablishmentsController : Controller
    {
        private readonly SeedingService _seedService;
        private readonly EstablishmentService _establishmentService;
        private readonly MovementService _movementService;
        private readonly CategoryService _categoryService;
        private readonly SubCategoryService _subCategoryService;

        public EstablishmentsController(SeedingService seedService,
                                        EstablishmentService establishmentService,
                                        MovementService movementService,
                                        CategoryService categoryService,
                                        SubCategoryService subCategoryService)
        {
            _seedService = seedService;
            _establishmentService = establishmentService;
            _movementService = movementService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        public IActionResult Index()
        {
            return View(_establishmentService.GetEstablishments());
        }

        public IActionResult Create()
        {
            var establishments = _establishmentService.GetEstablishments();
            var categories = _categoryService.GetCategories();
            var subCategories = _subCategoryService.GetSubCategories();

            var viewModel = new EstablishmentViewModel
            {
                Establishments = establishments,
                Categories = categories,
                SubCategories = subCategories
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Establishment establishment)
        {
            if (!ModelState.IsValid)
            {
                var categories = _categoryService.GetCategories();
                var subCategories = _subCategoryService.GetSubCategories();
                var viewModel = new EstablishmentViewModel
                {
                    Establishment = establishment,
                    Categories = categories,
                    SubCategories = subCategories
                };
                return View(viewModel);
            }
            _establishmentService.Insert(establishment);
            _movementService.UpdateEstablishments();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id) 
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = _establishmentService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            var establishments = _establishmentService.GetEstablishments();
            var categories = _categoryService.GetCategories();
            var subCategories = _subCategoryService.GetSubCategories();

            var viewModel = new EstablishmentViewModel
            {
                Establishment = obj,
                Establishments = establishments,
                Categories = categories,
                SubCategories = subCategories
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Establishment establishment)
        {
            if (!ModelState.IsValid)
            {
                var categories = _categoryService.GetCategories();
                var subCategories = _subCategoryService.GetSubCategories();
                var viewModel = new EstablishmentViewModel
                {
                    Establishment = establishment,
                    Categories = categories,
                    SubCategories = subCategories
                };
                return View(viewModel);
            }
            if (id != establishment.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível" });
            }
            try
            {
                _establishmentService.Update(establishment);
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
