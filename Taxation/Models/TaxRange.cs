namespace Taxation.Models
{
    public class TaxRange
    {
        public decimal LowerBound { get; set; }
        public decimal? UpperBound { get; set; }
        public decimal TaxRate { get; set; }
    }
}
