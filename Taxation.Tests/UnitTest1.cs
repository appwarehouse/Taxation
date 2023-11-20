using Taxation.Services;

namespace Taxation.Tests
{
    public class Tests
    {
        private TaxCalculator _taxCalculator;

        [SetUp]
        public void Setup()
        {
            _taxCalculator = new TaxCalculator();
        }

        [Test]
        public void CaculateFlatTax()
        {
            decimal income = 50000;
            decimal tax = _taxCalculator.CalculateTax("7000", income);
            Assert.That(tax, Is.EqualTo(ExpectedFlatRateTax(income)));
        }

        [Test]
        public void CalculateFlatValueBelow200000()
        {
            decimal income = 199000;
            decimal tax = _taxCalculator.CalculateTax("A100", income);
            Assert.That(tax, Is.EqualTo(ExpectedFixedValue(income)));
        }

        [Test]
        public void CalculateFlatValueAbove200000()
        {
            decimal income = 299000;
            decimal tax = _taxCalculator.CalculateTax("A100", income);
            Assert.That(tax, Is.EqualTo(ExpectedFixedValue(income)));
        }

        [Test]
        public void CalculateProgressiveBelowStatingRange()
        {
            decimal income = 8300;
            decimal tax = _taxCalculator.CalculateTax("1000", income);
            Assert.That(tax, Is.EqualTo(ExpectedProgressiveTax(income)));
        }

        [Test]
        public void CalculateProgressiveMidRange()
        {
            decimal income = 77562;
            decimal tax = _taxCalculator.CalculateTax("1000", income);
            Assert.That(tax, Is.EqualTo(ExpectedProgressiveTax(income)));
        }

        [Test]
        public void CalculateProgressiveAboveRange()
        {
            decimal income = 772950;
            decimal tax = _taxCalculator.CalculateTax("7441", income);
            Assert.That(tax, Is.EqualTo(ExpectedProgressiveTax(income)));
        }

        [Test]
        public void IncorrectPostalCode()
        {
            decimal income = 772950;
            decimal tax = _taxCalculator.CalculateTax("154", income);
            Assert.That(tax, Is.EqualTo(0));
        }

        [Test]
        public void CalculateProgressiveZero()
        {
            decimal income = 0;
            decimal tax = _taxCalculator.CalculateTax("7441", income);
            Assert.That(tax, Is.EqualTo(ExpectedProgressiveTax(income)));
        }

        private decimal ExpectedFlatRateTax(decimal income)
        {
            
            decimal taxRate = 0.175m; // 17.5%
            decimal flatRateTax = income * taxRate;

            return flatRateTax;
        }

        private decimal ExpectedFixedValue(decimal income)
        {
            if (income >= 200000)
                return 10000;
            return income * 0.05m;
        }
        private decimal ExpectedProgressiveTax(decimal annualIncome)
        {
            decimal taxPayable = 0m;

            if (annualIncome <= 8350)
            {
                taxPayable = annualIncome * 0.10m;
            }
            else if (annualIncome <= 33950)
            {
                taxPayable += 8350 * 0.10m;

                taxPayable += (annualIncome - 8350) * 0.15m;
            }
            else if (annualIncome <= 82250)
            {
                taxPayable += 8350 * 0.10m;

                taxPayable += (33950 - 8350) * 0.15m;

                taxPayable += (annualIncome - 33950) * 0.25m;
            }
            else if (annualIncome <= 171550)
            {
                taxPayable += 8350 * 0.10m;

                taxPayable += (33950 - 8350) * 0.15m;

                taxPayable += (82250 - 33950) * 0.25m;

                taxPayable += (annualIncome - 82250) * 0.28m;
            }
            else if (annualIncome <= 372950)
            {
                taxPayable += 8350 * 0.10m;

                taxPayable += (33950 - 8350) * 0.15m;

                taxPayable += (82250 - 33950) * 0.25m;

                taxPayable += (171550 - 82250) * 0.28m;

                taxPayable += (annualIncome - 171550) * 0.33m;
            }
            else
            {

                taxPayable += 8350 * 0.10m;

                taxPayable += (33950 - 8350) * 0.15m;

                taxPayable += (82250 - 33950) * 0.25m;

                taxPayable += (171550 - 82250) * 0.28m;

                taxPayable += (372950 - 171550) * 0.33m;

                taxPayable += (annualIncome - 372950) * 0.35m;
            }

            return taxPayable;
        }



    }
}