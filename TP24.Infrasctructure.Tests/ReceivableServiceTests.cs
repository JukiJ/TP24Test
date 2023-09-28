using AutoFixture;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using TP24.Core.Dtos;
using TP24.Core.Models;
using TP24.Data;
using TP24.Infrasctructure.Tests.Mocks;
using TP24.Infrastructure.Services;

namespace TP24.Infrasctructure.Tests
{
    public class ReceivableServiceTests
    {
        private readonly ReceivableRepositoryMock _repositoryMock = new ReceivableRepositoryMock();
        private ReceivableService _sut;

        public ReceivableServiceTests()
        {
            _sut = new ReceivableService(_repositoryMock.Object);
        }

        [Fact]
        public async Task ReceivablesService_GetReceivableSummary_ReturnsNumberOfClosedRecievables()
        {
            //Arrange
            var closedReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.ClosedDate, DateTime.Now)
                .With(x => x.Cancelled, false)
                .CreateMany(10).ToList();
            var openReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.ClosedDate, (DateTime?)null)
                .With(x => x.Cancelled, false)
                .CreateMany(10).ToList();
            var cancelledReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.ClosedDate, DateTime.Now)
                .With(x => x.PaidValue, (decimal)100.0)
                .With(x => x.Cancelled, true)
                .CreateMany(10).ToList();

            var allReceivables = new List<Receivable>(closedReceivables);
            allReceivables.AddRange(openReceivables);
            allReceivables.AddRange(cancelledReceivables);

            _repositoryMock.SetupGetReceivableSummary(allReceivables.BuildMock());

            //Act
            var result = await _sut.GetReceivableSummary();

            //Assert
            Assert.Equal(closedReceivables.Count, result.ClosedInvoicesNumber);
        }

        [Fact]
        public async Task ReceivablesService_GetReceivableSummary_ReturnsAmountOfClosedRecievables()
        {
            //Arrange
            var closedReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.ClosedDate, DateTime.Now)
                .With(x => x.PaidValue, (decimal)100.0)
                .With(x => x.Cancelled, false)
                .CreateMany(10).ToList();
            var openReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.ClosedDate, (DateTime?)null)
                .With(x => x.PaidValue, (decimal)100.0)
                .With(x => x.Cancelled, false)
                .CreateMany(10).ToList();
            var cancelledReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.ClosedDate, DateTime.Now)
                .With(x => x.PaidValue, (decimal)100.0)
                .With(x => x.Cancelled, true)
                .CreateMany(10).ToList();

            var allReceivables = new List<Receivable>(closedReceivables);
            allReceivables.AddRange(openReceivables);
            allReceivables.AddRange(cancelledReceivables);

            _repositoryMock.SetupGetReceivableSummary(allReceivables.BuildMock());

            //Act
            var result = await _sut.GetReceivableSummary();

            //Assert
            Assert.Equal((decimal)1000, result.ClosedInvoicesAmount);
        }


        [Theory]
        [InlineData(10, 10, 10)]
        [InlineData(20, 0, 10)]
        [InlineData(20, 10, 0)]
        [InlineData(0, 10, 10)]
        public async Task ReceivablesService_GetReceivableSummary_ReturnsNumberOfOpenRecievables(int openReceivablesCount, int closedReceivablesCount, int cancelledReceivablesCount)
        {
            //Arrange
            var closedReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.ClosedDate, DateTime.Now)
                .With(x => x.Cancelled, false)
                .CreateMany(closedReceivablesCount).ToList();
            var openReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.ClosedDate, (DateTime?)null)
                .With(x => x.Cancelled, false)
                .CreateMany(openReceivablesCount).ToList();
            var cancelledReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.Cancelled, true)
                .With(x => x.ClosedDate, (DateTime?)null)
                .With(x => x.OpeningValue, (decimal)100.0)
                .With(x => x.PaidValue, (decimal)10.0)
                .CreateMany(cancelledReceivablesCount).ToList();

            var allReceivables = new List<Receivable>(closedReceivables);
            allReceivables.AddRange(openReceivables);
            allReceivables.AddRange(cancelledReceivables);

            _repositoryMock.SetupGetReceivableSummary(allReceivables.BuildMock());

            //Act
            var result = await _sut.GetReceivableSummary();

            //Assert
            Assert.Equal(openReceivablesCount, result.OpenInvoicesNumber);
        }

        [Fact]
        public async Task ReceivablesService_GetReceivableSummary_ReturnsAmountOfOpenRecievables()
        {
            //Arrange
            var closedReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.Cancelled, false)
                .With(x => x.ClosedDate, DateTime.Now)
                .With(x => x.OpeningValue, (decimal)100.0)
                .With(x => x.PaidValue, (decimal)10.0)
                .CreateMany(10).ToList();
            var openReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.Cancelled, false)
                .With(x => x.ClosedDate, (DateTime?)null)
                .With(x => x.OpeningValue, (decimal)100.0)
                .With(x => x.PaidValue, (decimal)10.0)
                .CreateMany(10).ToList();
            var cancelledReceivables = new Fixture()
                .Build<Receivable>()
                .With(x => x.Cancelled, true)
                .With(x => x.ClosedDate, (DateTime?)null)
                .With(x => x.OpeningValue, (decimal)100.0)
                .With(x => x.PaidValue, (decimal)10.0)
                .CreateMany(10).ToList();

            var allReceivables = new List<Receivable>(closedReceivables);
            allReceivables.AddRange(openReceivables);
            allReceivables.AddRange(cancelledReceivables);

            _repositoryMock.SetupGetReceivableSummary(allReceivables.BuildMock());

            //Act
            var result = await _sut.GetReceivableSummary();

            //Assert
            Assert.Equal((decimal)1000, result.OpenInvoicesTotalAmount);
            Assert.Equal((decimal)900, result.OpenInvoicesUnpaidAmount);
        }

        [Theory]
        [InlineData("", "test", "test", "test", "test", 100)]
        [InlineData("test", "", "test", "test", "test", 100)]
        [InlineData("test", "test", "", "test", "test", 100)]
        [InlineData("test", "test", "test", "", "test", 100)]
        [InlineData("test", "test", "test", "test", "", 100)]
        [InlineData("test", "test", "test", "test", "test", 0)]
        public async Task ReceivablesService_AddReceivables_ThrowsExceptionOnWrongData(string dueDate, string debtorName, string debtorReference, string reference, string currencyCode, decimal openingValue)
        {
            //Arrange
            var closedReceivables = new Fixture()
                .Build<CreateReceivableDto>()
                .With(x => x.Cancelled, false)
                .With(x => x.ClosedDate, "")
                .With(x => x.DueDate, dueDate)
                .With(x => x.DebtorName, debtorName)
                .With(x => x.DebtorReference, debtorReference)
                .With(x => x.Reference, reference)
                .With(x => x.CurrencyCode, currencyCode)
                .With(x => x.OpeningValue, openingValue)
                .With(x => x.PaidValue, (decimal)10.0)
                .CreateMany(10).ToList();

            _repositoryMock.SetupSaveChangesAsync(closedReceivables.Count);

            //Act
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _sut.AddReceivablesAsync(closedReceivables));
        }

        [Fact]
        public async Task ReceivablesService_AddRecivables_ReturnsAmountOfAddedRecievables()
        {
            //Arrange
            var receivables = new Fixture()
                .Build<CreateReceivableDto>()
                .With(x => x.Cancelled, false)
                .With(x => x.ClosedDate, "")
                .With(x=>x.DueDate, "05/29/2015 5:50 AM")
                .With(x => x.OpeningValue, (decimal)100.0)
                .With(x => x.PaidValue, (decimal)10.0)
                .CreateMany(10).ToList();

            _repositoryMock.SetupSaveChangesAsync(receivables.Count);

            //Act
            var result = await _sut.AddReceivablesAsync(receivables);

            //Assert
            Assert.Equal(10, result);
        }
    }
}