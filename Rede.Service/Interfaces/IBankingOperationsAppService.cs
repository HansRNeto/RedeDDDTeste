using Rede.Service.EventSourcedNormalizers.BankingOperations;
using Rede.Service.EventSourcedNormalizers.CurrencyAccount;
using Rede.Service.ViewModels;

namespace Rede.Service.Interfaces;

public interface IBankingOperationsAppService: IDisposable
{
    void Register(BankingOperationsViewModel bankingOperationsViewModel);
    IEnumerable<BankingOperationsViewModel> GetAll();
    IEnumerable<BankingOperationsViewModel> GetAll(int skip, int take);
    BankingOperationsViewModel GetById(Guid id);
    

    void Update(BankingOperationsViewModel bankingOperationsViewModel);
    void Remove(Guid id);
    IList<BankingOperationsHistoryData> GetAllHistory(Guid id);
}