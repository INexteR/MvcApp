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

            int count = loan.Term / loan.Step;

            double i = loan.Bet / 100 * loan.Step;
            double K = i * double.Pow(1 + i, count) / (double.Pow(1 + i, count) - 1);
            double A = K * loan.Amount;

            var results = new Result[count];
            double remainingDebt = loan.Amount;
            for (int n = -1; ++n < count;)
            {
                double percentages = remainingDebt * i;
                double principalDebt = A - percentages;
                remainingDebt -= principalDebt;
                results[n] = new Result
                {
                    Date = DateTime.Now.AddDays((n + 1) * loan.Step),
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
            if (loan.Term % loan.Step != 0)
            {
                loan = new Loan
                {
                    Amount = loan.Amount,
                    Bet = loan.Bet,
                    Term = count * loan.Step,
                    Step = loan.Step
                };
            }
            ViewBag.Loan = loan;
            ViewBag.Annuity = double.Round(A, 2);
            ViewBag.Overpayment = double.Round(results.Sum(r => r.Percentages), 2);
            return View("Results", results.AsReadOnly());
        }
    }
}