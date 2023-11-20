namespace Taxation.Services
{
    public interface IPersonalTax
    {
        public decimal CalculateTax(string postalCode, decimal income);

    }
}
