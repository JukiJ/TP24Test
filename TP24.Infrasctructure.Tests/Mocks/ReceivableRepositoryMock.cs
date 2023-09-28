using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP24.Core.Interfaces.Repositories;
using TP24.Core.Models;

namespace TP24.Infrasctructure.Tests.Mocks
{
    public class ReceivableRepositoryMock : Mock<IReceivableRepository>
    {
        public void SetupGetReceivableSummary(IQueryable<Receivable> receivables)
        {
            Setup(x=>x.GetReceivables()).Returns(receivables);
        }

        public void SetupSaveChangesAsync(int numberOfItems)
        {
            Setup(x => x.SaveChangesAsync().Result).Returns(numberOfItems);
        }
    }
}
