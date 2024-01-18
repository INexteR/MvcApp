using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class Loan
    {
        public Loan(int amount, int term, int bet)
        {
            Amount = amount;
            Term = term;
            Bet = bet;
        }

        public Loan() { }

        [Range(5000, 10_000_000)]
        public int Amount { get; init; }
        [Range(1, 60)]
        public int Term { get; init; }
        [Range(10, 60)]
        public int Bet { get; init; }
       
    }
}
