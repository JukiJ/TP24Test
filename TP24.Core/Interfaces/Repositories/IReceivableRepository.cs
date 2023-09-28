using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP24.Core.Dtos;
using TP24.Core.Models;

namespace TP24.Core.Interfaces.Repositories
{
    public interface IReceivableRepository : IBaseRepository
    {
        IQueryable<Receivable> GetReceivables();
        Task AddReceivablesAsync(List<Receivable> receivable);
    }
}
