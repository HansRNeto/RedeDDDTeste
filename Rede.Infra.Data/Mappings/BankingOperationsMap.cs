using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rede.Domain.Models;

namespace Rede.Infra.Data.Mappings;

public class BankingOperationsMap : IEntityTypeConfiguration<BankingOperations>
{
    public void Configure(EntityTypeBuilder<BankingOperations> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.OriginAccount)
            .HasMaxLength(30)
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder.Property(c => c.DestinatioAccount)
            .HasMaxLength(30)
            .HasColumnType("varchar(30)")
            .IsRequired();
            
        builder.Property(c => c.Amount)
            .IsRequired();

        builder.Property(c => c.Operation)
            .HasMaxLength(30)
            .HasColumnType("varchar(30)")
            .IsRequired();
        

        // builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}