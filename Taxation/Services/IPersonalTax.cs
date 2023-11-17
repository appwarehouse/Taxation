namespace Taxation.Services
{
    public interface IPersonalTax
    {
        public decimal CalcualteTax(string postalCode, decimal income);

    }
}
