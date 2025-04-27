using System.Diagnostics;
using appdev_webapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace appdev_webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpensesDBContext _context; // read context for dependency injection

        public HomeController(ILogger<HomeController> logger, ExpensesDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expense()
        {
            var allExpenses = _context.Expenses.ToList();
            return View(allExpenses);
        }

        public IActionResult CreateEditExpense()
        {
            return View();
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            _context.Expenses.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Expense");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
