using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class Loan
    {
        public Loan() { }

        [Range(5000, 10_000_000)]
        public int Amount { get; init; }

        [Range(5, 1700)]
        public int Term { get; init; }

        [Range(0.001, 2)]
        public double Bet { get; init; }

        [Range(1, 30)]
        public int Step { get; init; } 
    }
}
