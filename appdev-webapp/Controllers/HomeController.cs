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

        public IActionResult Dashboard()
        {
            var expenses = _context.Expenses.ToList();

            var viewModel = new Dashboard // dashboard model
            {
                ExpenseCount = expenses.Count,
                TotalExpense = expenses.Sum(e => e.Value),
                AverageExpense = expenses.Any() ? expenses.Average(e => e.Value) : 0,
                MostExpensive = expenses.OrderByDescending(e => e.Value).FirstOrDefault()
            };


            return View(viewModel);
        }


        // GET: Home/CreateEditExpenseForm/5 or Home/CreateEditExpenseForm
        public IActionResult CreateEditExpense(int? id)
        {
            if (id == null)
                return View(new Expense()); // No ID provided -> Show blank form for creating

            var expense = _context.Expenses.Find(id);
            if (expense == null)
                return NotFound(); // ID provided but not found in DB

            return View(expense); // Load existing expense -> Show in edit mode
        }


        // save the form data, either create or update a new record
        [HttpPost]
        public IActionResult CreateEditExpense(Expense model)
        {
            if (model.Id == 0)
            {
                _context.Expenses.Add(model); // New expense -> Add to DB
            }
            else
            {
                _context.Expenses.Update(model); // Existing expense -> Update in DB
            }

            _context.SaveChanges(); // Save to DB
            return RedirectToAction("Expense"); // Go back to list view
        }



        // POST: /Expense/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null) return NotFound();

            _context.Expenses.Remove(expense);
            _context.SaveChanges();

            return RedirectToAction("Expense");
        }

        public IActionResult Expense()
        {
            var allExpenses = _context.Expenses.ToList();
            return View(allExpenses);
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
