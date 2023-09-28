using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP24.Core.Dtos
{
    public class ReceivableSummaryDto
    {
        public int OpenInvoicesNumber { get; set; }
        public int ClosedInvoicesNumber { get; set; }
        public decimal OpenInvoicesTotalAmount { get; set; }
        public decimal OpenInvoicesUnpaidAmount { get; set; }
        public decimal ClosedInvoicesAmount { get; set; }
    }
}
