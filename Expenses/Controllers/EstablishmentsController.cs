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
        private readonly KeyWordService _keyWordService;

        public EstablishmentsController(SeedingService seedService,
                                        EstablishmentService establishmentService,
                                        MovementService movementService,
                                        CategoryService categoryService,
                                        SubCategoryService subCategoryService,
                                        KeyWordService keyWordService)
        {
            _seedService = seedService;
            _establishmentService = establishmentService;
            _movementService = movementService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _keyWordService = keyWordService;
        }

        public IActionResult Index()
        {
            //_movementService.UpdateEstablishments();
            return View(_establishmentService.GetEstablishments());
        }

        public IActionResult Create()
        {
            var viewModel = new EstablishmentViewModel
            {
                KeyWords = _keyWordService.GetKeyWords().ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Establishment establishment, List<int> keys)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new EstablishmentViewModel
                {
                    Establishment = establishment,
                    KeyWords = _keyWordService.GetKeyWords().ToList()
            };
                return View(viewModel);
            }
            _establishmentService.Insert(establishment, keys);
            _movementService.UpdateEstablishments();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            Establishment obj = _establishmentService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            EstablishmentViewModel viewModel = new EstablishmentViewModel();
            viewModel.Establishment = obj;
            viewModel.KeyWords = _keyWordService.GetKeyWords().ToList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Establishment establishment, List<int> keys)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new EstablishmentViewModel
                {
                    Establishment = establishment
                };
                return View(viewModel);
            }
            if (id != establishment.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível" });
            }
            try
            {
                var updated = _establishmentService.Update(establishment, keys);
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
