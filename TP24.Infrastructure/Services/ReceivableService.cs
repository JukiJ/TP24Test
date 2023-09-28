using Microsoft.EntityFrameworkCore;
using TP24.Core.Dtos;
using TP24.Core.Interfaces.Repositories;
using TP24.Core.Interfaces.Services;
using TP24.Core.Models;

namespace TP24.Infrastructure.Services
{
    public class ReceivableService : IReceivableService
    {
        private readonly IReceivableRepository _receivableRepository;

        public ReceivableService(IReceivableRepository receivableRepository)
        {
            _receivableRepository = receivableRepository;
        }

        public async Task<ReceivableSummaryDto> GetReceivableSummary()
        {
            var query = _receivableRepository.GetReceivables().Where(x => !x.Cancelled).Select(x => new { x.ClosedDate, x.OpeningValue, x.PaidValue });

            var closedInvoicesQuery = query.Where(x => x.ClosedDate != null);

            var closedInvoicesAmount = await closedInvoicesQuery.SumAsync(x => x.PaidValue);
            var closedInvoicesCount = await closedInvoicesQuery.CountAsync();

            var openInvoicesQuery = query.Where(x => x.ClosedDate == null);

            var openInvoicesTotalAmount = await openInvoicesQuery.SumAsync(x => x.OpeningValue);
            var openInvoicesUnpaidAmount = await openInvoicesQuery.SumAsync(x => x.OpeningValue - x.PaidValue);
            var openInvoicesCount = await openInvoicesQuery.CountAsync();

            return new ReceivableSummaryDto()
            {
                ClosedInvoicesAmount = closedInvoicesAmount,
                ClosedInvoicesNumber = closedInvoicesCount,
                OpenInvoicesNumber = openInvoicesCount,
                OpenInvoicesTotalAmount = openInvoicesTotalAmount,
                OpenInvoicesUnpaidAmount = openInvoicesUnpaidAmount
            };
        }

        public async Task<int> AddReceivablesAsync(List<CreateReceivableDto> receivables)
        {
            if(receivables == null || receivables.Count == 0)
            {
                throw new ArgumentException("List of receivables must not be null or empty");
            }

            var receivablesModels = new List<Receivable>();

            foreach (var receivableDto in receivables)
            {
                if (string.IsNullOrWhiteSpace(receivableDto.Reference) ||
                    string.IsNullOrWhiteSpace(receivableDto.CurrencyCode) ||
                    string.IsNullOrWhiteSpace(receivableDto.IssueDate) ||
                    receivableDto.OpeningValue <= 0 ||
                    string.IsNullOrWhiteSpace(receivableDto.DueDate) ||
                    string.IsNullOrWhiteSpace(receivableDto.DebtorName) ||
                    string.IsNullOrWhiteSpace(receivableDto.DebtorReference) ||
                    string.IsNullOrWhiteSpace(receivableDto.DebtorCountryCode))
                {
                    throw new ArgumentException("One of the receivables is invalid!");
                }

                if (!DateTime.TryParse(receivableDto.DueDate, out var dueDate))
                {
                    throw new ArgumentException("Due date is required on all receivables!");
                }


                var model = new Receivable()
                {
                    Reference = receivableDto.Reference,
                    DebtorState = receivableDto.DebtorState,
                    DebtorReference = receivableDto.DebtorReference,
                    Cancelled = receivableDto.Cancelled ?? false,
                    CurrencyCode = receivableDto.CurrencyCode,
                    DebtorAddress1 = receivableDto.DebtorAddress1,
                    DebtorAddress2 = receivableDto.DebtorAddress2,
                    DebtorCountryCode = receivableDto.CurrencyCode,
                    DebtorName = receivableDto.DebtorName,
                    DebtorRegistrationNumber = receivableDto.DebtorRegistrationNumber,
                    DebtorTown = receivableDto.DebtorTown,
                    DebtorZip = receivableDto.DebtorZip,
                    DueDate = dueDate.ToUniversalTime(),
                    IssueDate = receivableDto.IssueDate,
                    OpeningValue = receivableDto.OpeningValue,
                    PaidValue = receivableDto.PaidValue
                };

                if (DateTime.TryParse(receivableDto.ClosedDate, out var closedDate))
                {
                    model.ClosedDate = closedDate.ToUniversalTime();
                }

                receivablesModels.Add(model);
            }
            await _receivableRepository.AddReceivablesAsync(receivablesModels);

            return await _receivableRepository.SaveChangesAsync();
        }
    }
}
