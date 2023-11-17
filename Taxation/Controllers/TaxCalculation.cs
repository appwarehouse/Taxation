using Microsoft.AspNetCore.Mvc;
using Taxation.Services;


namespace Taxation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculation : ControllerBase
    {
        private readonly IPersonalTax _personalTax;
        public TaxCalculation(IPersonalTax personalTax)
        {
            _personalTax = personalTax; 
        }

        [HttpGet("{postalCode}/{annualIncome}")]
        public decimal Get(string postalCode, decimal annualIncome)
        {
            return _personalTax.CalcualteTax(postalCode, annualIncome);
        }

    }
}
