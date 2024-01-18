using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            double i = loan.Bet / 100d / 12;
            double K = i * double.Pow(1 + i, loan.Term) / (double.Pow(1 + i, loan.Term) - 1);
            double A = K * loan.Amount;

            var results = new Result[loan.Term];
            double remainingDebt = loan.Amount;
            for (int n = -1; ++n < results.Length;)
            {
                double percentages = remainingDebt * i;
                double principalDebt = A - percentages;
                remainingDebt -= principalDebt;
                results[n] = new Result
                {
                    Date = DateTime.Now.AddMonths(n + 1),
                    PrincipalDebt = principalDebt,
                    Percentages = percentages,
                    RemainingDebt = remainingDebt
                };
            }
            if (results[^1].RemainingDebt < 0)
            {
                results[^1] = new Result
                {
                    Date = results[^1].Date,
                    PrincipalDebt = results[^1].PrincipalDebt,
                    Percentages = results[^1].Percentages,
                    RemainingDebt = 0
                };
            }
            ViewBag.Loan = loan;
            ViewBag.Annuity = double.Round(A, 2);
            ViewBag.Overpayment = double.Round(results.Sum(r => r.Percentages), 2);
            return View("Results", results.AsReadOnly());
        }
    }
}