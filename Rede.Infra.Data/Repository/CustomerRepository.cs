using Microsoft.EntityFrameworkCore;
using Rede.Domain.Interfaces;
using Rede.Domain.Models;
using Rede.Infra.Data.Context;

namespace Rede.Infra.Data.Repository;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(RedeContext context) : base(context)
    {
    }

    public Customer GetByEmail(string email)
    {
        return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email)!;
    }

    public Customer GetByDocument(string document)
    {
        return DbSet.AsNoTracking().Include(x => x.CurrencyAccount).FirstOrDefault(c => c.Document == document)!;
    }
}