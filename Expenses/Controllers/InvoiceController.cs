using Microsoft.AspNetCore.Mvc;

namespace Expenses.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }


    }
}
