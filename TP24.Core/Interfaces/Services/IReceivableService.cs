using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP24.Core.Dtos;
using TP24.Core.Models;

namespace TP24.Core.Interfaces.Services
{
    public interface IReceivableService
    {
        Task<ReceivableSummaryDto> GetReceivableSummary();
        Task<int> AddReceivablesAsync(List<CreateReceivableDto> receivableDto);
    }
}
