using Rede.Service.EventSourcedNormalizers;
using Rede.Service.EventSourcedNormalizers.Customer;
using Rede.Service.ViewModels;

namespace Rede.Service.Interfaces;

public interface ICustomerAppService : IDisposable
    {
        void Register(CustomerViewModel customerViewModel);
        IEnumerable<CustomerViewModel> GetAll();
        IEnumerable<CustomerViewModel> GetAll(int skip, int take);
        CustomerViewModel GetById(Guid id);
        CustomerViewModel GetByDocument(string document);
        void Update(CustomerViewModel customerViewModel);
        void Remove(Guid id);
        IList<CustomerHistoryData> GetAllHistory(Guid id);
    }
