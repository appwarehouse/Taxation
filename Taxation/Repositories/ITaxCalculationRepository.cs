using Taxation.Models;

namespace Taxation.Repositories
{
    public interface ITaxCalculationRepository
    {
        public Task<TaxCalculationRecord> InsertAsync(TaxCalculationRecord calculationRecord);
    }
}
