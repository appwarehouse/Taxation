namespace TaxationLib
{
    public class TaxCalculationRequest
    {
        public required string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
    }

}
