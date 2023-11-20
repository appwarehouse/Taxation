namespace Taxation.Models
{
    public class TaxCalculationRecord
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal CalculatedTax { get; set; }
        public DateTime CalculationDate { get; set; }
    }
}