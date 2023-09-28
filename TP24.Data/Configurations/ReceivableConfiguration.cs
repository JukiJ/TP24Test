using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP24.Core.Models;

namespace TP24.Data.Configurations
{
    public class ReceivableConfiguration : IEntityTypeConfiguration<Receivable>
    {
        public void Configure(EntityTypeBuilder<Receivable> builder)
        {
            builder.HasKey(x=> x.Id);

            builder.Property(x=>x.Reference).IsRequired();

            builder.Property(x => x.CurrencyCode).IsRequired();

            builder.Property(x => x.IssueDate).IsRequired();

            builder.Property(x => x.OpeningValue).IsRequired();

            builder.Property(x => x.DueDate).IsRequired();

            builder.Property(x => x.DebtorName).IsRequired();

            builder.Property(x => x.DebtorReference).IsRequired();

            builder.Property(x => x.DebtorCountryCode).IsRequired();
        }
    }
}
