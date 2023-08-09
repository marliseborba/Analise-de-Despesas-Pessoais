using Microsoft.AspNetCore.Mvc;
using Expenses.Services;
using Expenses.Models.ViewModels;

namespace Expenses.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoiceService _invoiceService;

        public InvoicesController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public IActionResult Index()
        {
            var viewModel = new InvoiceViewModel { Invoice = _invoiceService.Upload() };
            return View(viewModel);
        }


    }
}
