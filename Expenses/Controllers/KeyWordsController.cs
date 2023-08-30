using Expenses.Data;
using Expenses.Models;
using Expenses.Models.ViewModels;
using Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Expenses.Controllers
{
    public class KeyWordsController : Controller
    {
        private readonly SeedingService _seedService;
        private readonly KeyWordService _keyWordService;

        public KeyWordsController(SeedingService seedService, KeyWordService keyWordService)
        {
            _seedService = seedService;
            _keyWordService = keyWordService;
        }

        public IActionResult Index()
        {
            return View(_keyWordService.GetKeyWords());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string keys)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var created = _keyWordService.Insert(keys);
            TempData["created"] = created.Count;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = _keyWordService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            KeyWordViewModel viewModel = new KeyWordViewModel();
            viewModel.KeyWord = obj;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KeyWord keyWord)
        {
            if (!ModelState.IsValid)
            {
                KeyWordViewModel viewModel = new KeyWordViewModel();
                viewModel.KeyWord = keyWord;
                return View(viewModel);
            }
            if (id != keyWord.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível" });
            }
            try
            {
                var updated = _keyWordService.Update(keyWord);
                TempData["updated"] = updated.Description;
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
    }
}
