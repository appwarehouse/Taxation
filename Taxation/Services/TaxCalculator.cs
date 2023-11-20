using System.Diagnostics;
using Taxation.Models;

namespace Taxation.Services
{
    public class TaxCalculator : IPersonalTax
    {
        public decimal CalculateTax(string postalCode, decimal income)
        {
            switch (postalCode)
            {
                case "7441":
                case "1000":
                    return CalculateProgressiveTax(income);
                case "A100":
                    return CalculateFixedAmount(income);
                case "7000":
                    return CalculateFixedRate(income);
                default:
                    return 0;
            }
        }

        private decimal CalculateFixedAmount(decimal income)
        {
            return income < 200000 ? income * 0.05m : 10000;
        }
        private decimal CalculateFixedRate(decimal income)
        {
            return income * 0.175m;
        }
        private decimal CalculateProgressiveTax(decimal income)
        {
            List<TaxRange> incomeTaxRanges = new List<TaxRange>
            {
                new TaxRange { LowerBound = 0, UpperBound = 8350, TaxRate = 0.10m },
                new TaxRange { LowerBound = 8350, UpperBound = 33950, TaxRate = 0.15m },
                new TaxRange { LowerBound = 33950, UpperBound = 82250, TaxRate = 0.25m },
                new TaxRange { LowerBound = 82250, UpperBound = 171550, TaxRate = 0.28m },
                new TaxRange { LowerBound = 171550, UpperBound = 372950, TaxRate = 0.33m },
                new TaxRange { LowerBound = 372950, UpperBound = null, TaxRate = 0.35m }
            };


            decimal tax = incomeTaxRanges
                .Where(range => income > range.LowerBound)
                .Sum(range =>
                {
                    decimal taxableAmount = range.UpperBound.HasValue
                        ? Math.Min(range.UpperBound.Value, income) - range.LowerBound
                        : income - range.LowerBound;
                    var taxed = taxableAmount > 0 ? taxableAmount * range.TaxRate : 0;
                    Debug.WriteLine($"Taxed: {taxed} on {taxableAmount}");
                    return taxed;
                });

            return tax;
            

        }
    }

}
