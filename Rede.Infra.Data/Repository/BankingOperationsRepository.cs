using Rede.Domain.Interfaces;
using Rede.Domain.Models;
using Rede.Infra.Data.Context;

namespace Rede.Infra.Data.Repository;

public class BankingOperationsRepository : Repository<BankingOperations>, IBankingOperationsRepository
{
    public BankingOperationsRepository(RedeContext context) : base(context)
    {
    }
}