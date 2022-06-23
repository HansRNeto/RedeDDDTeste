using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rede.Domain.Models;

namespace Rede.Infra.Data.Mappings;

public class CurrencyAccountMap : IEntityTypeConfiguration<CurrencyAccount>
{
    public void Configure(EntityTypeBuilder<CurrencyAccount> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.NumberAccount)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(c => c.Digit)
            .HasMaxLength(2)
            .IsRequired();
            
        builder.Property(c => c.Balance)
            .IsRequired();

        builder.HasOne(a => a.Customer).WithOne(b => b.CurrencyAccount)
            .HasForeignKey<CurrencyAccount>(x => x.CustomerId);

        // builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}