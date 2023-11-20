using Microsoft.AspNetCore.Mvc;
using Taxation.Models;
using Taxation.Repositories;
using Taxation.Services;
using TaxationLib;


namespace Taxation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculation : ControllerBase
    {
        private readonly IPersonalTax _personalTax;
        private readonly ITaxCalculationRepository _taxCalculationRepository;
        public TaxCalculation(IPersonalTax personalTax, ITaxCalculationRepository taxCalculationRepository)
        {
            _personalTax = personalTax; 
            _taxCalculationRepository = taxCalculationRepository;
        }

        [HttpPost]
        public async Task<ActionResult<decimal>> Post([FromBody] TaxCalculationRequest request)
        {

            var taxableAmount = _personalTax.CalculateTax(request.PostalCode, request.AnnualIncome);
            var persisted = await _taxCalculationRepository.InsertAsync(new TaxCalculationRecord() 
            { 
                AnnualIncome = request.AnnualIncome, 
                PostalCode = request.PostalCode, 
                CalculatedTax=taxableAmount, 
                CalculationDate = DateTime.UtcNow,
            });
            return Ok(taxableAmount);
        }

    }
}
