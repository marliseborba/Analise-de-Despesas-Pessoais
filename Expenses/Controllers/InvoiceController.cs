using Microsoft.AspNetCore.Mvc;
using Expenses.Services;
using Expenses.Models.ViewModels;

namespace Expenses.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public IActionResult Index()
        {
            var viewModel = new InvoiceViewModel { Invoice = InvoiceService.Upload() };
            return View(viewModel);
        }


    }
}
