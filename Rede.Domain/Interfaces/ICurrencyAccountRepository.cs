using Rede.Domain.Models;

namespace Rede.Domain.Interfaces
{
    public interface ICurrencyAccountRepository : IRepository<CurrencyAccount>
    {
        CurrencyAccount GetByCustomerId(Guid customerId);
        CurrencyAccount GetByAccountNumber(int numberAccount, int digit);

    }
}