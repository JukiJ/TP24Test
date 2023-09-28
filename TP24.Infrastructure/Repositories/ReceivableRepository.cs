using TP24.Core.Interfaces.Repositories;
using TP24.Core.Models;
using TP24.Data;

namespace TP24.Infrastructure.Repositories
{
    public class ReceivableRepository : BaseRepository, IReceivableRepository
    {

        public ReceivableRepository(TP24DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Receivable> GetReceivables()
        {
            return _dbContext.Receivables;
        }

        public async Task AddReceivablesAsync(List<Receivable> receivable)
        {
            await _dbContext.AddRangeAsync(receivable);
        }
    }
}
