using Taxation.Data;
using Taxation.Models;

namespace Taxation.Repositories
{
    public class TaxCalculationsRepository : ITaxCalculationRepository
    {
        private readonly TaxationDBContext _dbContext;

        public TaxCalculationsRepository(TaxationDBContext dbContext)
        {
                _dbContext = dbContext;
        }
        public async  Task<TaxCalculationRecord> InsertAsync(TaxCalculationRecord calculationRecord)
        {
            var newRecord  = await _dbContext.TaxCalculations.AddAsync(calculationRecord);
            return newRecord.Entity;
        }
    }
}
