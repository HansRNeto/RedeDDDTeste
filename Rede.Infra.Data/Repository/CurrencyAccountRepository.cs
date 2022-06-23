using Microsoft.EntityFrameworkCore;
using Rede.Domain.Interfaces;
using Rede.Domain.Models;
using Rede.Infra.Data.Context;

namespace Rede.Infra.Data.Repository;

public class CurrencyAccountRepository : Repository<CurrencyAccount>, ICurrencyAccountRepository
{
    public CurrencyAccountRepository(RedeContext context) : base(context)
    {
    }

    public CurrencyAccount GetByCustomerId(Guid customerId)
    {
        return DbSet.AsNoTracking().FirstOrDefault(c => c.CustomerId == customerId)!;
    }

    public CurrencyAccount GetByAccountNumber(int numberAccount, int digit)
    {
        return DbSet.AsNoTracking().FirstOrDefault(c => c.NumberAccount == numberAccount && c.Digit == digit)!;
    }
}