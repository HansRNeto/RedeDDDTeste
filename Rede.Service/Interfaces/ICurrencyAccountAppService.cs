using Rede.Service.EventSourcedNormalizers.CurrencyAccount;
using Rede.Service.ViewModels;

namespace Rede.Service.Interfaces;

public interface ICurrencyAccountAppService: IDisposable
{
    void Register(CurrencyAccountViewModel currencyAccount);
    IEnumerable<CurrencyAccountViewModel> GetAll();
    IEnumerable<CurrencyAccountViewModel> GetAll(int skip, int take);
    CurrencyAccountViewModel GetById(Guid id);
    
    CurrencyAccountViewModel GetByCustomer(Guid customerId);
    
    CurrencyAccountViewModel GetByAccountNumber(string accountNumber);

    void Update(CurrencyAccountViewModel currencyAccount);
    void Remove(Guid id);
    IList<CurrencyAccountHistoryData> GetAllHistory(Guid id);
}